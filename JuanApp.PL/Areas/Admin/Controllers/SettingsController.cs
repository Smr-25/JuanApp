using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class SettingsController : Controller
{
    private readonly ISettingService _settingService;

    public SettingsController(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public async Task<IActionResult> Index()
    {
        var settings = await _settingService.GetAllSettingsAsync();
        return View(settings);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm]Dictionary<string, string> settings)
    {
        var result = await _settingService.UpdateSettingsAsync(settings);
        
        if (result)
        {
            TempData["Success"] = "Settings updated successfully!";
        }
        else
        {
            TempData["Error"] = "Failed to update settings!";
        }
        
        return RedirectToAction("Index","Dashboard");
    }
}

