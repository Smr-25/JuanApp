using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IBlogService
{
    Task<List<BlogDto>> GetAllAsync();
    Task<BlogDto?> GetByIdAsync(int id);
    Task<bool> CreateAsync(BlogCreateDto dto);
    Task<bool> UpdateAsync(BlogUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<List<BlogDto>> GetRecentBlogsAsync(int count);
    Task<List<BlogDto>> GetBlogsByCategoryAsync(string category);
    Task IncrementViewCountAsync(int id);
}

