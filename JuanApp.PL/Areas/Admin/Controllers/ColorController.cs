using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class ColorController(IAdminColorService colorService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var colors = await colorService.GetAllColorsAsync();
        return View(colors);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("Name", "Color name is required");
            return View();
        }

        await colorService.CreateColorAsync(name);
        TempData["Success"] = "Color created successfully!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var color = await colorService.GetColorByIdAsync(id);
        if (color == null) return NotFound();
        return View(color);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("Name", "Color name is required");
            var color = await colorService.GetColorByIdAsync(id);
            return View(color);
        }

        var result = await colorService.UpdateColorAsync(id, name);
        if (result)
        {
            TempData["Success"] = "Color updated successfully!";
            return RedirectToAction("Index");
        }

        TempData["Error"] = "Color not found";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await colorService.DeleteColorAsync(id);
        if (result)
        {
            TempData["Success"] = "Color deleted successfully!";
        }
        else
        {
            TempData["Error"] = "Color not found";
        }
        return RedirectToAction("Index");
    }
}

