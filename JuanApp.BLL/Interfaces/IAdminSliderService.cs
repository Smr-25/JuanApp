using JuanApp.BLL.Dtos;
using JuanApp.Domain.Models;

namespace JuanApp.BLL.Interfaces;

public interface IAdminSliderService
{
    Task<List<Slider>> GetAllSlidersAsync();
    Task<Slider?> GetSliderByIdAsync(int id);
    Task<bool> CreateSliderAsync(Slider dto, Microsoft.AspNetCore.Http.IFormFile? imageFile);
    Task<bool> UpdateSliderAsync(int id, Slider dto, Microsoft.AspNetCore.Http.IFormFile? imageFile);
    Task<bool> DeleteSliderAsync(int id);
}

