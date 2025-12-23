using JuanApp.Domain.Models;

namespace JuanApp.Core.Models;

public class ProductReview
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int Rating { get; set; }  // 1-5
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation
    public Product Product { get; set; } = null!;
    public AppUser User { get; set; } = null!;
}

