namespace JuanApp.BLL.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public string? SelectedColor { get; set; }
    public string? SelectedSize { get; set; }
    public decimal TotalPrice { get; set; }
}
