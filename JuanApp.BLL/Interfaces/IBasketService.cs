using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IBasketService
{
    Task<BasketDto> GetBasketAsync(string userId);
    Task<bool> AddToBasketAsync(string userId, AddToBasketDto dto);
    Task<bool> UpdateQuantityAsync(string userId, int basketItemId, int quantity);
    Task<bool> RemoveFromBasketAsync(string userId, int basketItemId);
    Task<bool> ClearBasketAsync(string userId);
    Task<int> GetBasketItemCountAsync(string userId);
}