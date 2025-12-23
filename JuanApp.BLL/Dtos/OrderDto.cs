namespace JuanApp.BLL.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public string UserFullName { get; set; }
    public string UserEmail { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShippingAddress { get; set; }
    public string ContactPhone { get; set; }

    public List<OrderItemDto> OrderItems { get; set; }
}


