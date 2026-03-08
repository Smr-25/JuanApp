using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class AdminSizeService(AppDbContext context) : IAdminSizeService
{
    public async Task<List<ProductSizeDto>> GetAllSizesAsync()
    {
        var sizes = await context.Sizes.ToListAsync();
        return sizes.Select(s => new ProductSizeDto
        {
            Id = s.Id,
            Name = s.Name
        }).ToList();
    }

    public async Task<ProductSizeDto?> GetSizeByIdAsync(int id)
    {
        var size = await context.Sizes.FindAsync(id);
        if (size == null) return null;

        return new ProductSizeDto
        {
            Id = size.Id,
            Name = size.Name
        };
    }

    public async Task<bool> CreateSizeAsync(string name)
    {
        var size = new Size { Name = name };
        context.Sizes.Add(size);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateSizeAsync(int id, string name)
    {
        var size = await context.Sizes.FindAsync(id);
        if (size == null) return false;

        size.Name = name;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteSizeAsync(int id)
    {
        var size = await context.Sizes.FindAsync(id);
        if (size == null) return false;

        context.Sizes.Remove(size);
        await context.SaveChangesAsync();
        return true;
    }
}

