using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class AdminColorService(AppDbContext context) : IAdminColorService
{
    public async Task<List<ProductColorDto>> GetAllColorsAsync()
    {
        var colors = await context.Colors.ToListAsync();
        return colors.Select(c => new ProductColorDto
        {
            Id = c.Id,
            Name = c.Name,
            HexCode = c.HexCode
        }).ToList();
    }

    public async Task<ProductColorDto?> GetColorByIdAsync(int id)
    {
        var color = await context.Colors.FindAsync(id);
        if (color == null) return null;

        return new ProductColorDto
        {
            Id = color.Id,
            Name = color.Name,
            HexCode = color.HexCode
        };
    }

    public async Task<bool> CreateColorAsync(string name, string hexCode)
    {
        var color = new Color 
        { 
            Name = name,
            HexCode = hexCode
        };
        context.Colors.Add(color);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateColorAsync(int id, string name, string hexCode)
    {
        var color = await context.Colors.FindAsync(id);
        if (color == null) return false;

        color.Name = name;
        color.HexCode = hexCode;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteColorAsync(int id)
    {
        var color = await context.Colors.FindAsync(id);
        if (color == null) return false;

        context.Colors.Remove(color);
        await context.SaveChangesAsync();
        return true;
    }
}

