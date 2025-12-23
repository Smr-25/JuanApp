using JuanApp.DLL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.PL.Controllers;

public class BlogController : Controller
{
    private readonly AppDbContext _context;

    public BlogController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _context.Blogs
            .OrderByDescending(b => b.PublishedDate)
            .ToListAsync();
        return View(blogs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return NotFound();
        }

        // Increment view count
        blog.ViewCount++;
        await _context.SaveChangesAsync();

        return View(blog);
    }
}

