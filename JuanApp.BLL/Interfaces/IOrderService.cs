using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task<bool> UpdateOrderStatusAsync(int id, string status);
    Task<List<OrderDto>> GetUserOrdersAsync(string userId);
    Task<int> CreateOrderAsync(string userId, CheckoutDto dto);
}
