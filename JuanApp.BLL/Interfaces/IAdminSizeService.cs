using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IAdminSizeService
{
    Task<List<ProductSizeDto>> GetAllSizesAsync();
    Task<ProductSizeDto?> GetSizeByIdAsync(int id);
    Task<bool> CreateSizeAsync(string name);
    Task<bool> UpdateSizeAsync(int id, string name);
    Task<bool> DeleteSizeAsync(int id);
}

