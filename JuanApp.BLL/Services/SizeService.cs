using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class SizeService(AppDbContext db) : ISizeService
{
    public async Task<List<ProductSizeDto>> GetAllSizesAsync()
    {
        var sizes = await db.Sizes.ToListAsync();
        var sizeDtos = sizes.Select(s => new ProductSizeDto
        {
            Id = s.Id,
            Name = s.SizeType
        }).ToList();
        return sizeDtos;
    }
}