namespace JuanApp.BLL.Dtos;

public class BasketDto
{
    public List<BasketItemDto> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Subtotal);
    public int TotalItems => Items.Sum(x => x.Quantity);
}