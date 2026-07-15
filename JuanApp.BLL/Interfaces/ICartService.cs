using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface ICartService
{
    Task<CartMockResponseDto> GetMockCartAsync();
}

