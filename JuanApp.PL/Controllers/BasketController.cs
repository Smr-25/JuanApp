using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class BasketController(
    IBasketService basketService,
    UserManager<AppUser> userManager,
    IOrderService orderService,
    ISubscriberService subscriberService) : Controller
{
    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = userManager.GetUserId(User);
        var basket = await basketService.GetBasketAsync(userId);
        
        return View(basket);
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket([FromBody] AddToBasketDto dto)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { success = false, message = "Please login first" });
        }

        var userId = userManager.GetUserId(User);
        var result = await basketService.AddToBasketAsync(userId, dto);

        if (result)
        {
            var itemCount = await basketService.GetBasketItemCountAsync(userId);
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

        var userId = userManager.GetUserId(User);
        var result = await basketService.UpdateQuantityAsync(userId, basketItemId, quantity);

        if (result)
        {
            var basket = await basketService.GetBasketAsync(userId);
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

        var userId = userManager.GetUserId(User);
        var result = await basketService.RemoveFromBasketAsync(userId, basketItemId);

        if (result)
        {
            var basket = await basketService.GetBasketAsync(userId);
            return Json(new 
            { 
                success = true, 
                itemCount = basket.TotalItems,
                totalPrice = basket.TotalPrice
            });
        }

        return Json(new { success = false, message = "Failed to remove item" });
    }

    [HttpGet]
    public async Task<IActionResult> GetItemCount()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { itemCount = 0 });
        }

        var userId = userManager.GetUserId(User);
        var count = await basketService.GetBasketItemCountAsync(userId);
        return Json(new { itemCount = count });
    }

    [HttpGet]
    public async Task<IActionResult> GetBasketItems()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { success = false, requiresLogin = true });
        }

        var userId = userManager.GetUserId(User);
        var basket = await basketService.GetBasketAsync(userId);
        
        var items = basket.Items.Select(i => new
        {
            id = i.Id,
            productName = i.ProductName,
            price = i.Price,
            quantity = i.Quantity,
            imageUrl = i.ProductImage
        }).ToList();

        return Json(new { success = true, items, totalPrice = basket.TotalPrice });
    }

    [HttpPost]
    public async Task<IActionResult> Clear()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = userManager.GetUserId(User);
        await basketService.ClearBasketAsync(userId);

        return RedirectToAction("Index");
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
            var userId = userManager.GetUserId(User);
            var orderId = await orderService.CreateOrderAsync(userId, dto);

            if (dto.Subscribe && !string.IsNullOrEmpty(dto.Email))
            {
                await subscriberService.SubscribeAsync(dto.Email);
            }

            return Json(new { success = true, message = "Order placed successfully", orderId });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "An error occurred: " + ex.Message });
        }
    }
}

