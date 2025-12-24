using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class BlogController : Controller
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _blogService.GetAllAsync();
        return View(blogs);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BlogCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var result = await _blogService.CreateAsync(dto);
        if (result)
        {
            TempData["Success"] = "Blog created successfully!";
            return RedirectToAction("Index");
        }

        TempData["Error"] = "Failed to create blog!";
        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var blog = await _blogService.GetByIdAsync(id);
        if (blog == null) return NotFound();

        var dto = new BlogUpdateDto
        {
            Id = blog.Id,
            Title = blog.Title,
            Description = blog.Description,
            Content = blog.Content,
            Author = blog.Author,
            Category = blog.Category,
            Tags = blog.Tags,
            ExistingImageUrl = blog.ImageUrl
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BlogUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var result = await _blogService.UpdateAsync(dto);
        if (result)
        {
            TempData["Success"] = "Blog updated successfully!";
            return RedirectToAction("Index");
        }

        TempData["Error"] = "Failed to update blog!";
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _blogService.DeleteAsync(id);
        if (result)
        {
            return Json(new { success = true, message = "Blog deleted successfully" });
        }

        return Json(new { success = false, message = "Failed to delete blog" });
    }
}

