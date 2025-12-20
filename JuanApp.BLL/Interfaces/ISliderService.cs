using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface ISliderService
{
    Task<HomeSliderDto> GetAllSlidersAsync();
}