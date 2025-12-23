using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IAdminSliderService
{
    Task<List<HomeSliderDto>> GetAllSlidersAsync();
    Task<HomeSliderDto?> GetSliderByIdAsync(int id);
    Task<bool> CreateSliderAsync(HomeSliderDto dto, Microsoft.AspNetCore.Http.IFormFile? imageFile);
    Task<bool> UpdateSliderAsync(int id, HomeSliderDto dto, Microsoft.AspNetCore.Http.IFormFile? imageFile);
    Task<bool> DeleteSliderAsync(int id);
}

