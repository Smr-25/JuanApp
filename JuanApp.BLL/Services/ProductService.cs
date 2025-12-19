using JuanApp.BLL.Dtos;
using JuanApp.DLL.Data;
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
}
