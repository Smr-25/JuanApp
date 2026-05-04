using System.ComponentModel.DataAnnotations;

namespace JuanApp.BLL.Dtos;

public class AddToBasketDto
{
    [Required(ErrorMessage = "Product ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Product ID must be a valid positive number")]
    public int ProductId { get; set; }

    [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999")]
    public int Quantity { get; set; } = 1;

    [Range(0, int.MaxValue, ErrorMessage = "Invalid Color ID")]
    public int? ColorId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Invalid Size ID")]
    public int? SizeId { get; set; }
}