namespace JuanApp.BLL.Dtos;

public sealed record CartItemDto(
    int ProductId,
    string Name,
    string ImageUrl,
    decimal Price,
    int Quantity);

public sealed record CartMockResponseDto(
    IReadOnlyList<CartItemDto> Items,
    decimal Subtotal,
    decimal Total);

