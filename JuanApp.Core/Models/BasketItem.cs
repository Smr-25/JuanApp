using JuanApp.Core.Common;
using JuanApp.Domain.Models;

namespace JuanApp.Core.Models;

public class BasketItem : BaseEntity
{
    public int BasketId { get; set; }
    public Basket Basket { get; set; }
    
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public int Quantity { get; set; }
    public decimal Price { get; set; }  
    
    public int? ColorId { get; set; }
    public Color Color { get; set; }
    
    public int? SizeId { get; set; }
    public Size Size { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}