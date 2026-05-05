using System.ComponentModel.DataAnnotations;

namespace JuanApp.BLL.Dtos;

public class ProductReviewCreateDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Product ID must be valid")]
    public int ProductId { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars")]
    public int Rating { get; set; }

    [StringLength(1000, MinimumLength = 5, ErrorMessage = "Comment must be between 5 and 1000 characters")]
    public string? Comment { get; set; }
}

