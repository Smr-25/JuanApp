using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class DashboardService(AppDbContext context) : IDashboardService
{
    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        return new DashboardStatsDto
        {
            ProductCount = await context.Products.CountAsync(),
            OrderCount = await context.Orders.CountAsync(),
            SubscriberCount = await context.Subscribers.CountAsync(),
            TotalRevenue = await context.Orders
                .Where(o => o.Status == OrderStatus.Processing)
                .SumAsync(o => o.TotalAmount)
        };
    }

    public async Task<List<RecentOrderDto>> GetRecentOrdersAsync(int count = 5)
    {
        return await context.Orders
            .Include(o => o.User)
            .OrderByDescending(o => o.OrderDate)
            .Take(count)
            .Select(o => new RecentOrderDto
            {
                Id = o.Id,
                CustomerName = o.User != null ? o.User.FullName : "Unknown User",
                TotalPrice = o.TotalAmount,
                Status = o.Status.ToString(),
                CreatedAt = o.OrderDate
            })
            .ToListAsync();
    }

    public async Task<List<PopularProductDto>> GetPopularProductsAsync(int count = 5)
    {
        return await context.Products
            .OrderByDescending(p => p.Price)
            .Take(count)
            .Select(p => new PopularProductDto
            {
                Name = p.Name,
                ImageUrl = p.ProductImages.FirstOrDefault() != null 
                    ? p.ProductImages.FirstOrDefault()!.ImageUrl 
                    : "assets/img/no-image.jpg",
                Price = p.Price
            })
            .ToListAsync();
    }
}

