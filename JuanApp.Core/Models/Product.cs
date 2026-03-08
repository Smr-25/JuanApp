using JuanApp.Core.Common;
using JuanApp.Core.Models;

namespace JuanApp.Domain.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public bool InStock { get; set; }
    public int DiscountPercentage { get; set; }
    public bool IsNew { get; set; }
    public bool IsMain { get; set; }
    public int SalesCount { get; set; } = 0;
    public List<ProductImage> ProductImages { get; set; } = new();
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Color> Colors { get; set; } = new();
    public List<Size> Sizes { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
    public List<ProductReview> Reviews { get; set; } = new();
    public List<BasketItem> BasketItems { get; set; } = new();
}

