using JuanApp.Core.Common;

namespace JuanApp.Core.Models;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShippingAddress { get; set; }
    public string ContactPhone { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

