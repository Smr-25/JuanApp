using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JuanApp.Controllers;

public class BasketController : Controller
{
    private readonly IBasketService _basketService;
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;
    private readonly ISubscriberService _subscriberService;

    public BasketController(
        IBasketService basketService,
        UserManager<AppUser> userManager,
        AppDbContext context,
        ISubscriberService subscriberService)
    {
        _basketService = basketService;
        _userManager = userManager;
        _context = context;
        _subscriberService = subscriberService;
    }
   
    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = _userManager.GetUserId(User);
        var basket = await _basketService.GetBasketAsync(userId);
        
        return View(basket);
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket([FromBody] AddToBasketDto dto)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { success = false, message = "Please login first" });
        }

        var userId = _userManager.GetUserId(User);
        var result = await _basketService.AddToBasketAsync(userId, dto);

        if (result)
        {
            var itemCount = await _basketService.GetBasketItemCountAsync(userId);
            return Json(new { success = true, message = "Product added to cart", itemCount });
        }

        return Json(new { success = false, message = "Failed to add product" });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int basketItemId, int quantity)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { success = false, message = "Unauthorized" });
        }

        var userId = _userManager.GetUserId(User);
        var result = await _basketService.UpdateQuantityAsync(userId, basketItemId, quantity);

        if (result)
        {
            var basket = await _basketService.GetBasketAsync(userId);
            return Json(new 
            { 
                success = true, 
                itemCount = basket.TotalItems,
                totalPrice = basket.TotalPrice
            });
        }

        return Json(new { success = false, message = "Failed to update quantity" });
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int basketItemId)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { success = false, message = "Unauthorized" });
        }

        var userId = _userManager.GetUserId(User);
        var result = await _basketService.RemoveFromBasketAsync(userId, basketItemId);

        if (result)
        {
            var basket = await _basketService.GetBasketAsync(userId);
            return Json(new 
            { 
                success = true, 
                itemCount = basket.TotalItems,
                totalPrice = basket.TotalPrice
            });
        }

        return Json(new { success = false, message = "Failed to remove item" });
    }

    [HttpPost]
    public async Task<IActionResult> Clear()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = _userManager.GetUserId(User);
        await _basketService.ClearBasketAsync(userId);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetItemCount()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { itemCount = 0 });
        }

        var userId = _userManager.GetUserId(User);
        var itemCount = await _basketService.GetBasketItemCountAsync(userId);

        return Json(new { itemCount });
    }

    [HttpGet]
    public async Task<IActionResult> GetBasketItems()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { success = false, message = "Unauthorized" });
        }

        var userId = _userManager.GetUserId(User);
        var basket = await _basketService.GetBasketAsync(userId);

        return Json(new { success = true, items = basket.Items });
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] CheckoutDto dto)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { success = false, message = "Unauthorized" });
        }

        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null || !basket.BasketItems.Any())
            {
                return Json(new { success = false, message = "Your cart is empty" });
            }

            // Create Order
            var order = new Order
            {
                UserId = userId,
                OrderNumber = "ORD-" + DateTime.Now.Ticks,
                OrderDate = DateTime.UtcNow,
                TotalAmount = basket.BasketItems.Sum(bi => bi.Price * bi.Quantity),
                Status = OrderStatus.Pending,
                ShippingAddress = dto.Address,
                ContactPhone = dto.Phone
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Create Order Items
            foreach (var item in basket.BasketItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    SelectedColor = item.Color?.Name ?? "",
                    SelectedSize = item.Size?.Name ?? ""
                };
                _context.OrderItems.Add(orderItem);
            }

            // Clear basket
            _context.BasketItems.RemoveRange(basket.BasketItems);
            await _context.SaveChangesAsync();

            // Subscribe if requested
            if (dto.Subscribe)
            {
                await _subscriberService.SubscribeAsync(dto.Email);
            }

            return Json(new { success = true, message = "Order placed successfully", orderId = order.Id });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "An error occurred: " + ex.Message });
        }
    }
}