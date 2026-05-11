using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class SizeService(AppDbContext db) : ISizeService
{
    public async Task<List<ProductSizeDto>> GetAllSizesAsync()
    {
        var sizes = await db.Sizes
            .Select(s => new ProductSizeDto
            {
                Id = s.Id,
                Name = s.Name,
                ProductCount = db.Products.Include(p => p.Sizes).Count(p => p.Sizes.Any(size => size.Id == s.Id))
            })
            .ToListAsync();
        
        return sizes;
    }
}