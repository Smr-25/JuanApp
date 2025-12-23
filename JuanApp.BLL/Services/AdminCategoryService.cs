using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class AdminCategoryService(AppDbContext context) : IAdminCategoryService
{
    public async Task<List<ProductCategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await context.Categories.ToListAsync();
        return categories.Select(c => new ProductCategoryDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }

    public async Task<ProductCategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return null;

        return new ProductCategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<bool> CreateCategoryAsync(string name)
    {
        var category = new Category { Name = name };
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateCategoryAsync(int id, string name)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return false;

        category.Name = name;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return false;

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return true;
    }
}

