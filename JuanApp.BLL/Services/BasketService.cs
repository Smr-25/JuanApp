using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class BasketService(AppDbContext db) : IBasketService
{
    public async Task<BasketDto> GetBasketAsync(string userId)
    {
        var basket = await db.Baskets
            .Include(b => b.BasketItems)
            .ThenInclude(bi => bi.Product)
            .ThenInclude(p => p.ProductImages)
            .Include(b => b.BasketItems)
            .ThenInclude(bi => bi.Color)
            .Include(b => b.BasketItems)
            .ThenInclude(bi => bi.Size)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        if (basket == null)
            return new BasketDto();

        return new BasketDto
        {
            Items = basket.BasketItems.Select(bi => new BasketItemDto
            {
                Id = bi.Id,
                ProductId = bi.ProductId,
                ProductName = bi.Product.Name,
                ProductImage = bi.Product.ProductImages.FirstOrDefault()?.ImageUrl,
                Price = bi.Price,
                Quantity = bi.Quantity,
                ColorName = bi.Color?.Name,
                SizeName = bi.Size?.Name
            }).ToList()
        };
    }

    public async Task<bool> AddToBasketAsync(string userId, AddToBasketDto dto)
    {
        var basket = await db.Baskets
            .Include(b => b.BasketItems)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        if (basket == null)
        {
            basket = new Basket { UserId = userId };
            db.Baskets.Add(basket);
            await db.SaveChangesAsync();
        }

        var product = await db.Products.FindAsync(dto.ProductId);
        if (product == null) return false;

        var existingItem = basket.BasketItems.FirstOrDefault(bi =>
            bi.ProductId == dto.ProductId &&
            bi.ColorId == dto.ColorId &&
            bi.SizeId == dto.SizeId);

        if (existingItem != null)
        {
            existingItem.Quantity += dto.Quantity;
            existingItem.Price = product.Price; // Update price
        }
        else
        {
            var basketItem = new BasketItem
            {
                BasketId = basket.Id,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Price = product.Price,
                ColorId = dto.ColorId,
                SizeId = dto.SizeId
            };
            db.BasketItems.Add(basketItem);
        }

        basket.UpdatedDate = DateTime.Now;
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateQuantityAsync(string userId, int basketItemId, int quantity)
    {
        var basketItem = await db.BasketItems
            .Include(bi => bi.Basket)
            .FirstOrDefaultAsync(bi => bi.Id == basketItemId && bi.Basket.UserId == userId);

        if (basketItem == null) return false;

        if (quantity <= 0)
        {
            db.BasketItems.Remove(basketItem);
        }
        else
        {
            basketItem.Quantity = quantity;
        }

        if (basketItem.Basket != null)
        {
            basketItem.Basket.UpdatedDate = DateTime.Now;
        }
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveFromBasketAsync(string userId, int basketItemId)
    {
        var basketItem = await db.BasketItems
            .Include(bi => bi.Basket)
            .FirstOrDefaultAsync(bi => bi.Id == basketItemId && bi.Basket.UserId == userId);

        if (basketItem == null) return false;

        db.BasketItems.Remove(basketItem);
        if (basketItem.Basket != null)
        {
            basketItem.Basket.UpdatedDate = DateTime.Now;
        }
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearBasketAsync(string userId)
    {
        var basket = await db.Baskets
            .Include(b => b.BasketItems)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        if (basket == null) return false;

        db.BasketItems.RemoveRange(basket.BasketItems);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetBasketItemCountAsync(string userId)
    {
        return await db.BasketItems
            .Where(bi => bi.Basket.UserId == userId)
            .SumAsync(bi => bi.Quantity);
    }
}