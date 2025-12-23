using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class SubscriberController(ISubscriberService subscriberService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var subscribers = await subscriberService.GetAllSubscribersAsync();
        return View(subscribers);
    }

    [HttpPost]
    public async Task<IActionResult> Unsubscribe(int id)
    {
        var result = await subscriberService.UnsubscribeAsync(id);
        if (result)
        {
            TempData["Success"] = "Subscriber removed successfully!";
        }
        else
        {
            TempData["Error"] = "Failed to remove subscriber";
        }
        return RedirectToAction("Index");
    }
}

