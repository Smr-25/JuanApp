using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class ColorService(AppDbContext db) : IColorService
{
    public async Task<List<ProductColorDto>> GetAllColorsAsync()
    {
        var colors = await db.Colors.ToListAsync();
        var colorDtos = colors.Select(c => new ProductColorDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
        return colorDtos;
    }
}