using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class ProductDetailsService(AppDbContext db) : IProductDetailsService
{
    public async Task<ProductDetailsDto> GetProductDetailsAsync(int productId)
    {
        var product = await db.Products
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == productId);
        if(product == null)
        {
            return null;
        }
        var relatedProducts = await db.Products
            .Where(p => p.Id != productId)
            .Take(4)
            .ToListAsync();
        if (relatedProducts == null)
        {
            return null;
        }
        var productDetailsDto = new ProductDetailsDto
        {
            Product =  product,
            RelatedProducts = relatedProducts
        };
        return productDetailsDto;
    }
}

