using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class SettingsController : Controller
{
    private readonly AppDbContext _context;

    public SettingsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var settings = await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
        return View(settings);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Dictionary<string, string> settings)
    {
        foreach (var setting in settings)
        {
            var existingSetting = await _context.Settings.FirstOrDefaultAsync(s => s.Key == setting.Key);
            if (existingSetting != null)
            {
                existingSetting.Value = setting.Value;
            }
            else
            {
                _context.Settings.Add(new Setting 
                { 
                    Key = setting.Key, 
                    Value = setting.Value 
                });
            }
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "Settings updated successfully!";
        return RedirectToAction("Index");
    }
}

