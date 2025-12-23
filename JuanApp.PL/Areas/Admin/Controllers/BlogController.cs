using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class BlogController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public BlogController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _context.Blogs.OrderByDescending(b => b.PublishedDate).ToListAsync();
        return View(blogs);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Blog blog, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            return View(blog);
        }

        if (imageFile != null)
        {
            blog.ImageUrl = await SaveImageAsync(imageFile);
        }

        blog.PublishedDate = DateTime.UtcNow;
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Blog created successfully!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return NotFound();
        return View(blog);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Blog blog, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            return View(blog);
        }

        var existingBlog = await _context.Blogs.FindAsync(blog.Id);
        if (existingBlog == null) return NotFound();

        existingBlog.Title = blog.Title;
        existingBlog.Description = blog.Description;
        existingBlog.Content = blog.Content;
        existingBlog.Author = blog.Author;
        existingBlog.Category = blog.Category;

        if (imageFile != null)
        {
            if (!string.IsNullOrEmpty(existingBlog.ImageUrl))
            {
                DeleteImage(existingBlog.ImageUrl);
            }
            existingBlog.ImageUrl = await SaveImageAsync(imageFile);
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "Blog updated successfully!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return Json(new { success = false, message = "Blog not found" });
        }

        if (!string.IsNullOrEmpty(blog.ImageUrl))
        {
            DeleteImage(blog.ImageUrl);
        }

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Blog deleted successfully" });
    }

    private async Task<string> SaveImageAsync(IFormFile file)
    {
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "blogs");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return "uploads/blogs/" + uniqueFileName;
    }

    private void DeleteImage(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl)) return;

        var fullPath = Path.Combine(_env.WebRootPath, imageUrl);
        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
        }
    }
}

