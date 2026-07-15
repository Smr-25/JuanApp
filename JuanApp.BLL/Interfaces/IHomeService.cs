using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IHomeService
{
    Task<HomeResponseDto> GetHomeAsync();
}

