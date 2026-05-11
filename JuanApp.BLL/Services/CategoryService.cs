using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class CategoryService(AppDbContext db) : ICategoryService
{
    public async Task<List<ProductCategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await db.Categories
            .Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ProductCount = db.Products.Count(p => p.CategoryId == c.Id)
            })
            .ToListAsync();
        
        return categories;
    }
}