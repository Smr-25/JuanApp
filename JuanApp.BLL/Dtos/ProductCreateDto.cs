using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace JuanApp.BLL.Dtos;

public class ProductCreateDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    
    public IFormFile? MainImage { get; set; }
    public List<IFormFile>? AdditionalImages { get; set; }
    
    public bool InStock { get; set; } = true;
    
    [Range(0, 100)]
    public int DiscountPercentage { get; set; }
    
    public bool IsNew { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    
    public List<int>? ColorIds { get; set; }
    public List<int>? SizeIds { get; set; }
}

public class ProductUpdateDto : ProductCreateDto
{
    public int Id { get; set; }
    public string? ExistingImageUrl { get; set; }
    public List<string>? ExistingAdditionalImages { get; set; }
}

