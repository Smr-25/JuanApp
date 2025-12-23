namespace JuanApp.BLL.Dtos;

public class AddToBasketDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 1;
    public int? ColorId { get; set; }
    public int? SizeId { get; set; }
}