using Microsoft.AspNetCore.Http;

namespace JuanApp.BLL.Dtos;

public class ProductUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool InStock { get; set; }
    public int DiscountPercentage { get; set; }
    public bool IsNew { get; set; }
    public bool IsMain { get; set; }
    public int CategoryId { get; set; }
    public List<int>? ColorIds { get; set; } = new();
    public List<int>? SizeIds { get; set; } = new();
    public List<int>? TagIds { get; set; } = new();
    public IFormFile? MainImage { get; set; } 
    public List<IFormFile>? AdditionalImages { get; set; }
    public List<IFormFile>? Images { get; set; }
    public List<string>? ExistingImages { get; set; }
    public string? ExistingImageUrl { get; set; }
}

