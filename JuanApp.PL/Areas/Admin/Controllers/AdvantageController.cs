using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class AdvantageController(IAdminAdvantageService advantageService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var advantages = await advantageService.GetAllAdvantagesAsync();
        return View(advantages);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(HomeAdvantageDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await advantageService.CreateAdvantageAsync(dto);
        TempData["Success"] = "Advantage created successfully!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var advantage = await advantageService.GetAdvantageByIdAsync(id);
        if (advantage == null) return NotFound();
        return View(advantage);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, HomeAdvantageDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var result = await advantageService.UpdateAdvantageAsync(id, dto);
        if (result)
        {
            TempData["Success"] = "Advantage updated successfully!";
            return RedirectToAction("Index");
        }

        TempData["Error"] = "Advantage not found";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await advantageService.DeleteAdvantageAsync(id);
        if (result)
        {
            TempData["Success"] = "Advantage deleted successfully!";
        }
        else
        {
            TempData["Error"] = "Advantage not found";
        }
        return RedirectToAction("Index");
    }
}
