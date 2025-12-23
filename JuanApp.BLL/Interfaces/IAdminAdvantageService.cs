using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IAdminAdvantageService
{
    Task<List<HomeAdvantageDto>> GetAllAdvantagesAsync();
    Task<HomeAdvantageDto?> GetAdvantageByIdAsync(int id);
    Task<bool> CreateAdvantageAsync(HomeAdvantageDto dto);
    Task<bool> UpdateAdvantageAsync(int id, HomeAdvantageDto dto);
    Task<bool> DeleteAdvantageAsync(int id);
}

