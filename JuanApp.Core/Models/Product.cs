using JuanApp.Core.Common;

namespace JuanApp.Domain.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public bool InStock { get; set; }
    public int DiscountPercentage { get; set; }
    List<ProductImage> ProductImages { get; set; }
}