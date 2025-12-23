using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IAdminColorService
{
    Task<List<ProductColorDto>> GetAllColorsAsync();
    Task<ProductColorDto?> GetColorByIdAsync(int id);
    Task<bool> CreateColorAsync(string name, string hexCode);
    Task<bool> UpdateColorAsync(int id, string name, string hexCode);
    Task<bool> DeleteColorAsync(int id);
}

