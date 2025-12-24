using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.ViewComponents;

public class MiniCartViewComponent : ViewComponent
{
    private readonly IBasketService _basketService;
    private readonly UserManager<AppUser> _userManager;

    public MiniCartViewComponent(IBasketService basketService, UserManager<AppUser> userManager)
    {
        _basketService = basketService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return View("Default", new { ItemCount = 0, Items = new List<object>() });
        }

        var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
        var basket = await _basketService.GetBasketAsync(userId);

        return View("Default", basket);
    }
}

