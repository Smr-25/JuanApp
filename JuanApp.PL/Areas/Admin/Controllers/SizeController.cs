using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class SizeController(IAdminSizeService sizeService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var sizes = await sizeService.GetAllSizesAsync();
        return View(sizes);
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
            ModelState.AddModelError("Name", "Size name is required");
            return View();
        }

        await sizeService.CreateSizeAsync(name);
        TempData["Success"] = "Size created successfully!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var size = await sizeService.GetSizeByIdAsync(id);
        if (size == null) return NotFound();
        return View(size);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("Name", "Size name is required");
            var size = await sizeService.GetSizeByIdAsync(id);
            return View(size);
        }

        var result = await sizeService.UpdateSizeAsync(id, name);
        if (result)
        {
            TempData["Success"] = "Size updated successfully!";
            return RedirectToAction("Index");
        }

        TempData["Error"] = "Size not found";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await sizeService.DeleteSizeAsync(id);
        if (result)
        {
            TempData["Success"] = "Size deleted successfully!";
        }
        else
        {
            TempData["Error"] = "Size not found";
        }
        return RedirectToAction("Index");
    }
}

