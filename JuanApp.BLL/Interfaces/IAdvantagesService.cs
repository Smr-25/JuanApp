using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IAdvantagesService
{
    Task<HomeAdvantageDto> GetAllAdvantagesAsync();
}