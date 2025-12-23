using JuanApp.Core.Common;
using JuanApp.Domain.Models;

namespace JuanApp.Core.Models;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? SelectedColor { get; set; }
    public string? SelectedSize { get; set; }
}

