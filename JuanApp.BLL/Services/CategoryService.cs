using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class CategoryService(AppDbContext db) : ICategoryService
{
    public async Task<List<ProductCategoryDto>> GetAllCategoriesAsync()
    {
        
        var categories = await db.Categories.ToListAsync();
        var categoryDtos = categories.Select(c => new ProductCategoryDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
        return categoryDtos;
    }
}