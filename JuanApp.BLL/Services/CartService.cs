using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;

namespace JuanApp.BLL.Services;

public sealed class CartService : ICartService
{
    public Task<CartMockResponseDto> GetMockCartAsync()
    {
        var items = new List<CartItemDto>
        {
            new(1, "Classic Jacket", "/images/products/product-1.jpg", 129.99m, 1),
            new(2, "Modern Sneakers", "/images/products/product-2.jpg", 89.50m, 2)
        };

        var subtotal = items.Sum(item => item.Price * item.Quantity);

        return Task.FromResult(new CartMockResponseDto(items, subtotal, subtotal));
    }
}

