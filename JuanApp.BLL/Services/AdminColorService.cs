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
            Name = c.Name
        }).ToList();
    }

    public async Task<ProductColorDto?> GetColorByIdAsync(int id)
    {
        var color = await context.Colors.FindAsync(id);
        if (color == null) return null;

        return new ProductColorDto
        {
            Id = color.Id,
            Name = color.Name
        };
    }

    public async Task<bool> CreateColorAsync(string name)
    {
        var color = new Color { Name = name };
        context.Colors.Add(color);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateColorAsync(int id, string name)
    {
        var color = await context.Colors.FindAsync(id);
        if (color == null) return false;

        color.Name = name;
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

