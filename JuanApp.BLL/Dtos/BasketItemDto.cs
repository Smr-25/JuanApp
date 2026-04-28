namespace JuanApp.BLL.Dtos;

public class BasketItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductImage { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ColorName { get; set; }
    public string? SizeName { get; set; }
    public decimal Subtotal => Price * Quantity;
}