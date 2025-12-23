using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class BasketController(IBasketService basketService,UserManager<AppUser> userManager) : Controller
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
    public async Task<IActionResult> AddToCart([FromBody] AddToBasketDto dto)
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
    public async Task<IActionResult> GetItemCount()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { itemCount = 0 });
        }

        var userId = userManager.GetUserId(User);
        var itemCount = await basketService.GetBasketItemCountAsync(userId);

        return Json(new { itemCount });
    }
}