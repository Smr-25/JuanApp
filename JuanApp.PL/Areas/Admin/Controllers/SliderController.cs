using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class SliderController(IAdminSliderService sliderService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var sliders = await sliderService.GetAllSlidersAsync();
        return View(sliders);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(HomeSliderDto dto, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await sliderService.CreateSliderAsync(dto, imageFile);
        TempData["Success"] = "Slider created successfully!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var slider = await sliderService.GetSliderByIdAsync(id);
        if (slider == null) return NotFound();
        return View(slider);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, HomeSliderDto dto, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var result = await sliderService.UpdateSliderAsync(id, dto, imageFile);
        if (result)
        {
            TempData["Success"] = "Slider updated successfully!";
            return RedirectToAction("Index");
        }

        TempData["Error"] = "Slider not found";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await sliderService.DeleteSliderAsync(id);
        if (result)
        {
            TempData["Success"] = "Slider deleted successfully!";
        }
        else
        {
            TempData["Error"] = "Slider not found";
        }
        return RedirectToAction("Index");
    }
}

