using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using JuanApp.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class AdminProductService(
    AppDbContext context,
    IWebHostEnvironment env,
    ISubscriberService subscriberService,
    IEmailService emailService) : IAdminProductService
{
    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .ToListAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            InStock = p.InStock,
            DiscountPercentage = p.DiscountPercentage,
            IsNew = p.IsNew,
            CategoryName = p.Category?.Name
        }).ToList();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            InStock = product.InStock,
            DiscountPercentage = product.DiscountPercentage,
            IsNew = product.IsNew,
            CategoryName = product.Category?.Name
        };
    }

    public async Task<ProductUpdateDto?> GetProductForEditAsync(int id)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return null;

        return new ProductUpdateDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            InStock = product.InStock,
            DiscountPercentage = product.DiscountPercentage,
            IsNew = product.IsNew,
            CategoryId = product.CategoryId,
            ColorIds = product.Colors.Select(c => c.Id).ToList(),
            SizeIds = product.Sizes.Select(s => s.Id).ToList(),
            ExistingImageUrl = product.ImageUrl
        };
    }

    public async Task<bool> CreateProductAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            InStock = dto.InStock,
            DiscountPercentage = dto.DiscountPercentage,
            IsNew = dto.IsNew,
            CategoryId = dto.CategoryId,
            IsMain = false
        };

        // Upload main image
        if (dto.MainImage != null)
        {
            product.ImageUrl = await SaveFileAsync(dto.MainImage);
        }

        context.Products.Add(product);
        await context.SaveChangesAsync();

        // Upload additional images
        if (dto.AdditionalImages != null && dto.AdditionalImages.Any())
        {
            foreach (var image in dto.AdditionalImages)
            {
                var imageUrl = await SaveFileAsync(image);
                context.ProductImages.Add(new ProductImage
                {
                    ProductId = product.Id,
                    ImageUrl = imageUrl
                });
            }
            await context.SaveChangesAsync();
        }

        // Add colors
        if (dto.ColorIds != null && dto.ColorIds.Any())
        {
            var colors = await context.Colors.Where(c => dto.ColorIds.Contains(c.Id)).ToListAsync();
            product.Colors = colors;
            await context.SaveChangesAsync();
        }

        // Add sizes
        if (dto.SizeIds != null && dto.SizeIds.Any())
        {
            var sizes = await context.Sizes.Where(s => dto.SizeIds.Contains(s.Id)).ToListAsync();
            product.Sizes = sizes;
            await context.SaveChangesAsync();
        }

        // Send email to subscribers
        await SendNewProductEmailToSubscribersAsync(product);

        return true;
    }

    public async Task<bool> UpdateProductAsync(ProductUpdateDto dto)
    {
        var product = await context.Products
            .Include(p => p.ProductImages)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefaultAsync(p => p.Id == dto.Id);

        if (product == null) return false;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.InStock = dto.InStock;
        product.DiscountPercentage = dto.DiscountPercentage;
        product.IsNew = dto.IsNew;
        product.CategoryId = dto.CategoryId;

        // Update main image if new one provided
        if (dto.MainImage != null)
        {
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                DeleteFile(product.ImageUrl);
            }
            product.ImageUrl = await SaveFileAsync(dto.MainImage);
        }

        // Update colors
        if (dto.ColorIds != null)
        {
            product.Colors.Clear();
            var colors = await context.Colors.Where(c => dto.ColorIds.Contains(c.Id)).ToListAsync();
            product.Colors = colors;
        }

        // Update sizes
        if (dto.SizeIds != null)
        {
            product.Sizes.Clear();
            var sizes = await context.Sizes.Where(s => dto.SizeIds.Contains(s.Id)).ToListAsync();
            product.Sizes = sizes;
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await context.Products
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return false;

        // Delete images
        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            DeleteFile(product.ImageUrl);
        }

        foreach (var img in product.ProductImages)
        {
            DeleteFile(img.ImageUrl);
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return true;
    }

    private async Task<string> SaveFileAsync(Microsoft.AspNetCore.Http.IFormFile file)
    {
        var uploadsFolder = Path.Combine(env.WebRootPath, "uploads", "products");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return "/uploads/products/" + uniqueFileName;
    }

    private void DeleteFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return;

        var fullPath = Path.Combine(env.WebRootPath, filePath.TrimStart('/'));
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    private async Task SendNewProductEmailToSubscribersAsync(Product product)
    {
        try
        {
            var subscribers = await subscriberService.GetActiveSubscriberEmailsAsync();
            if (!subscribers.Any()) return;

            var productLink = $"https://localhost:7062/Product/Details/{product.Id}";
            
            foreach (var email in subscribers)
            {
                var emailBody = $@"
                    <h2>New Product Alert! 🎉</h2>
                    <h3>{product.Name}</h3>
                    <p>{product.Description}</p>
                    <p><strong>Price: ${product.Price}</strong></p>
                    <a href='{productLink}' style='background-color: #4CAF50; color: white; padding: 14px 20px; text-decoration: none; display: inline-block;'>
                        View Product
                    </a>
                ";

                await emailService.SendEmailAsync(email, $"New Product: {product.Name}", emailBody);
            }
        }
        catch (Exception ex)
        {
            // Log error but don't fail product creation
            Console.WriteLine($"Failed to send subscriber emails: {ex.Message}");
        }
    }
}

