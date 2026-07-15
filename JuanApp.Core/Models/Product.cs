using JuanApp.Core.Common;

namespace JuanApp.Core.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal Rating { get; set; }
    public bool InStock { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }
    public int SortOrder { get; set; }
    public ICollection<ProductImage> GalleryImages { get; set; } = new List<ProductImage>();
}

