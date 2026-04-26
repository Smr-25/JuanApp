using JuanApp.Core.Common;

namespace JuanApp.Core.Models;

public class Basket : BaseEntity
{
    public string? UserId { get; set; }  
    public AppUser? User { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; }
    
    public ICollection<BasketItem>? BasketItems { get; set; }
}