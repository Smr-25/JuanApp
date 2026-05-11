namespace JuanApp.BLL.Interfaces;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetDashboardStatsAsync();
    Task<List<RecentOrderDto>> GetRecentOrdersAsync(int count = 5);
    Task<List<PopularProductDto>> GetPopularProductsAsync(int count = 5);
}

public class DashboardStatsDto
{
    public int ProductCount { get; set; }
    public int OrderCount { get; set; }
    public int SubscriberCount { get; set; }
    public decimal TotalRevenue { get; set; }
}

public class RecentOrderDto
{
    public int Id { get; set; }
    public string? CustomerName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class PopularProductDto
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int SalesCount { get; set; }
}

