using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Controllers;

public class SubscriberController : Controller
{
    private readonly ISubscriberService _subscriberService;

    public SubscriberController(ISubscriberService subscriberService)
    {
        _subscriberService = subscriberService;
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Json(new { success = false, message = "Email is required" });
        }

        var result = await _subscriberService.SubscribeAsync(email);
        if (result)
        {
            return Json(new { success = true, message = "Thank you for subscribing!" });
        }

        return Json(new { success = false, message = "This email is already subscribed" });
    }

    [HttpGet]
    public async Task<IActionResult> Unsubscribe(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            TempData["Error"] = "Invalid email address";
            return RedirectToAction("Index", "Home");
        }

        var result = await _subscriberService.UnsubscribeAsync(email);
        if (result)
        {
            TempData["Success"] = "You have been unsubscribed successfully";
        }
        else
        {
            TempData["Error"] = "Email not found in our subscriber list";
        }

        return RedirectToAction("Index", "Home");
    }
}

