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
}