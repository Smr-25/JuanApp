using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class ProductController : Controller
{
    private readonly IAdminProductService _adminProductService;
    private readonly ICategoryService _categoryService;
    private readonly IColorService _colorService;
    private readonly ISizeService _sizeService;

    public ProductController(
        IAdminProductService adminProductService,
        ICategoryService categoryService,
        IColorService colorService,
        ISizeService sizeService)
    {
        _adminProductService = adminProductService;
        _categoryService = categoryService;
        _colorService = colorService;
        _sizeService = sizeService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _adminProductService.GetAllProductsAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Colors = await _colorService.GetAllColorsAsync();
        ViewBag.Sizes = await _sizeService.GetAllSizesAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Colors = await _colorService.GetAllColorsAsync();
            ViewBag.Sizes = await _sizeService.GetAllSizesAsync();
            return View(dto);
        }

        var result = await _adminProductService.CreateProductAsync(dto);

        if (result)
        {
            TempData["Success"] = "Product created successfully and email sent to subscribers!";
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", "Failed to create product");
        ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Colors = await _colorService.GetAllColorsAsync();
        ViewBag.Sizes = await _sizeService.GetAllSizesAsync();
        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _adminProductService.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Colors = await _colorService.GetAllColorsAsync();
        ViewBag.Sizes = await _sizeService.GetAllSizesAsync();

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Colors = await _colorService.GetAllColorsAsync();
            ViewBag.Sizes = await _sizeService.GetAllSizesAsync();
            return View(dto);
        }

        var result = await _adminProductService.UpdateProductAsync(dto);

        if (result)
        {
            TempData["Success"] = "Product updated successfully!";
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", "Failed to update product");
        ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Colors = await _colorService.GetAllColorsAsync();
        ViewBag.Sizes = await _sizeService.GetAllSizesAsync();
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _adminProductService.DeleteProductAsync(id);

        if (result)
        {
            TempData["Success"] = "Product deleted successfully!";
        }
        else
        {
            TempData["Error"] = "Failed to delete product";
        }

        return RedirectToAction("Index");
    }
}