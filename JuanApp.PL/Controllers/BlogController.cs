using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Controllers;

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

    public async Task<IActionResult> Details(int id)
    {
        var blog = await _blogService.GetByIdAsync(id);
        if (blog == null)
        {
            return NotFound();
        }

        // Increment view count
        await _blogService.IncrementViewCountAsync(id);

        return View(blog);
    }
}

