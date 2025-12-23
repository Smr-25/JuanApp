using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IAdminCategoryService
{
    Task<bool> DeleteCategoryAsync(int id);
    Task<bool> UpdateCategoryAsync(int id, string name);
    Task<bool> CreateCategoryAsync(string name);
    Task<ProductCategoryDto?> GetCategoryByIdAsync(int id);
    Task<List<ProductCategoryDto>> GetAllCategoriesAsync();
}