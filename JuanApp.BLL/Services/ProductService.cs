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
        var product = await db.Products
            .Include(p => p.ProductImages)
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
            .FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null)
        {
            return null;
        }

        var relatedProducts = await db.Products
            .Where(p => p.Id != productId && p.CategoryId == product.CategoryId)
            .Take(4)
            .ToListAsync();
        if (relatedProducts == null)
        {
            return null;
        }

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
            .AsQueryable();

        if (productFilterDto.CategoryId != 0)
        {
            query = query.Where(p => p.CategoryId == productFilterDto.CategoryId);
        }

        if (productFilterDto.SizeId != 0)
        {
            query = query.Where(p => p.Sizes.Any(s => s.Id == productFilterDto.SizeId));
        }

        if (productFilterDto.ColorId != 0)
        {
            query = query.Where(p => p.Colors.Any(c => c.Id == productFilterDto.ColorId));
        }

        switch (productFilterDto.SortBy)
        {
            case "price_asc":
                query = query.OrderBy(p => p.Price);
                break;
            case "price_desc":
                query = query.OrderByDescending(p => p.Price);
                break;
            case "name_asc":
                query = query.OrderBy(p => p.Name);
                break;
            case "name_desc":
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
                ImageUrl = p.ProductImages.FirstOrDefault().ImageUrl
            })
            .ToListAsync();

        return (products, totalCount);
    }
}