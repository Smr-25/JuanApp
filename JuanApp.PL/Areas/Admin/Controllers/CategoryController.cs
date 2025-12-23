using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class CategoryController(IAdminCategoryService categoryService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        return View(categories);
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
            ModelState.AddModelError("Name", "Category name is required");
            return View();
        }

        await categoryService.CreateCategoryAsync(name);
        TempData["Success"] = "Category created successfully!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await categoryService.GetCategoryByIdAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("Name", "Category name is required");
            var category = await categoryService.GetCategoryByIdAsync(id);
            return View(category);
        }

        var result = await categoryService.UpdateCategoryAsync(id, name);
        if (result)
        {
            TempData["Success"] = "Category updated successfully!";
            return RedirectToAction("Index");
        }

        TempData["Error"] = "Category not found";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await categoryService.DeleteCategoryAsync(id);
        if (result)
        {
            TempData["Success"] = "Category deleted successfully!";
        }
        else
        {
            TempData["Error"] = "Category not found";
        }
        return RedirectToAction("Index");
    }
}

