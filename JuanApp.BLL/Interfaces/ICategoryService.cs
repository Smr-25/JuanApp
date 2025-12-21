using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface ICategoryService
{
    Task<List<ProductCategoryDto>> GetAllCategoriesAsync();
}