using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class OrderService(AppDbContext context) : IOrderService
{
    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            UserId = o.UserId,
            OrderNumber = o.OrderNumber,
            UserFullName = o.User.FullName,
            UserEmail = o.User.Email,
            TotalAmount = o.TotalAmount,
            TotalPrice = o.TotalAmount,
            CreatedAt = o.OrderDate,
            Status = o.Status.ToString(),
            OrderDate = o.OrderDate,
            ShippingAddress = o.ShippingAddress,
            ContactPhone = o.ContactPhone,
            OrderItems = o.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductName = oi.Product.Name,
                Quantity = oi.Quantity,
                Price = oi.Price,
                TotalPrice = oi.Price * oi.Quantity,
                SelectedColor = oi.SelectedColor,
                SelectedSize = oi.SelectedSize
            }).ToList()
        }).ToList();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return null;

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderNumber = order.OrderNumber,
            UserFullName = order.User.FullName,
            UserEmail = order.User.Email,
            TotalAmount = order.TotalAmount,
            TotalPrice = order.TotalAmount,
            CreatedAt = order.OrderDate,
            Status = order.Status.ToString(),
            OrderDate = order.OrderDate,
            ShippingAddress = order.ShippingAddress,
            ContactPhone = order.ContactPhone,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductName = oi.Product.Name,
                Quantity = oi.Quantity,
                Price = oi.Price,
                TotalPrice = oi.Price * oi.Quantity,
                SelectedColor = oi.SelectedColor,
                SelectedSize = oi.SelectedSize
            }).ToList()
        };
    }

    public async Task<bool> UpdateOrderStatusAsync(int id, string status)
    {
        var order = await context.Orders.FindAsync(id);
        if (order == null) return false;

        if (Enum.TryParse<OrderStatus>(status, out var orderStatus))
        {
            order.Status = orderStatus;
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<List<OrderDto>> GetUserOrdersAsync(string userId)
    {
        var orders = await context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            UserId = o.UserId,
            OrderNumber = o.OrderNumber,
            UserFullName = o.User.FullName,
            UserEmail = o.User.Email,
            TotalAmount = o.TotalAmount,
            TotalPrice = o.TotalAmount,
            CreatedAt = o.OrderDate,
            Status = o.Status.ToString(),
            OrderDate = o.OrderDate,
            ShippingAddress = o.ShippingAddress,
            ContactPhone = o.ContactPhone,
            OrderItems = o.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductName = oi.Product.Name,
                Quantity = oi.Quantity,
                Price = oi.Price,
                TotalPrice = oi.Price * oi.Quantity,
                SelectedColor = oi.SelectedColor,
                SelectedSize = oi.SelectedSize
            }).ToList()
        }).ToList();
    }
}

