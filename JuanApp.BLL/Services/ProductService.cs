using JuanApp.BLL.Dtos;
using JuanApp.DLL.Data;
using JuanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Interfaces;

public class ProductService(AppDbContext db) : IProductService
{
    public async Task<HomeProductDto> GetAllProductsAsync()
    {
        var mainProducts = await db.Products
            .Where(p => p.IsMain)
            .ToListAsync();
        var newProducts = await db.Products.Where(p => p.IsNew).ToListAsync();
        return new HomeProductDto
        {
            MainProducts = mainProducts,
            NewProducts = newProducts
        };
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var product =await db.Products
            .Include(p => p.ProductImages)
            .Include(p => p.Category)
            .Include(p => p.Sizes)
            .Include(p => p.Colors)
            .FirstOrDefaultAsync(p => p.Id == productId); 
        if (product == null)
            return null;

        return product;
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await db.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
        return products;
    }

    public async Task<ProductDetailsDto> GetProductDetailsAsync(int productId)
    {
        var product = await db.Products
            .Include(p => p.ProductImages)
            .Include(p => p.Category)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null)
        {
            return null;
        }

        var relatedProducts = await db.Products
            .Include(p => p.ProductImages)
            .Include(p => p.Category)
            .Where(p => p.Id != productId && p.CategoryId == product.CategoryId)
            .Take(4)
            .ToListAsync();

        var productDetailsDto = new ProductDetailsDto
        {
            Product = product,
            RelatedProducts = relatedProducts
        };
        return productDetailsDto;
    }

    public async Task<(IEnumerable<ProductDto> Products, int TotalCount)> GetFilteredProductsAsync(
        ProductFilterDto productFilterDto)
    {
        var query = db.Products
            .Include(p => p.Category)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .Include(p => p.ProductImages) // image lazımdır
            .AsQueryable();

        if (productFilterDto.CategoryId.HasValue && productFilterDto.CategoryId.Value != 0)
        {
            query = query.Where(p => p.CategoryId == productFilterDto.CategoryId.Value);
        }

        if (productFilterDto.ColorId != null && productFilterDto.ColorId.Any())
        {
            query = query.Where(p => p.Colors.Any(c => productFilterDto.ColorId.Contains(c.Id)));
        }

        if (productFilterDto.SizeId != null && productFilterDto.SizeId.Any())
        {
            query = query.Where(p => p.Sizes.Any(s => productFilterDto.SizeId.Contains(s.Id)));
        }

        if (productFilterDto.MinPrice.HasValue)
            query = query.Where(p => p.Price >= productFilterDto.MinPrice.Value);

        if (productFilterDto.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= productFilterDto.MaxPrice.Value);

        switch (productFilterDto.SortBy?.ToLower())
        {
            case "price-asc":
                query = query.OrderBy(p => p.Price);
                break;
            case "price-desc":
                query = query.OrderByDescending(p => p.Price);
                break;
            case "name-asc":
                query = query.OrderBy(p => p.Name);
                break;
            case "name-desc":
                query = query.OrderByDescending(p => p.Name);
                break;
            default:
                query = query.OrderBy(p => p.Id);
                break;
        }

        var totalCount = await query.CountAsync();

        var products = await query
            .Skip((productFilterDto.Page - 1) * productFilterDto.PageSize)
            .Take(productFilterDto.PageSize)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                DiscountPercentage = p.DiscountPercentage,
                ImageUrl = p.ProductImages.FirstOrDefault().ImageUrl,
                Description = p.Description
            })
            .ToListAsync();

        return (products, totalCount);
    }
}