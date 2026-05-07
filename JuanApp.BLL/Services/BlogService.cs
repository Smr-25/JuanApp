using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class BlogService(AppDbContext context, IWebHostEnvironment env) : IBlogService
{
    public async Task<List<BlogDto>> GetAllAsync()
    {
        return await context.Blogs
            .OrderByDescending(b => b.PublishedDate)
            .Select(b => new BlogDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Content = b.Content,
                ImageUrl = b.ImageUrl,
                Author = b.Author,
                PublishedDate = b.PublishedDate,
                ViewCount = b.ViewCount,
                Category = b.Category,
                Tags = b.Tags
            })
            .ToListAsync();
    }

    public async Task<BlogDto?> GetByIdAsync(int id)
    {
        var blog = await context.Blogs.FindAsync(id);
        if (blog == null) return null;

        return new BlogDto
        {
            Id = blog.Id,
            Title = blog.Title,
            Description = blog.Description,
            Content = blog.Content,
            ImageUrl = blog.ImageUrl,
            Author = blog.Author,
            PublishedDate = blog.PublishedDate,
            ViewCount = blog.ViewCount,
            Category = blog.Category,
            Tags = blog.Tags
        };
    }

    public async Task<bool> CreateAsync(BlogCreateDto dto)
    {
        try
        {
            var blog = new Blog
            {
                Title = dto.Title,
                Description = dto.Description,
                Content = dto.Content,
                Author = dto.Author,
                Category = dto.Category,
                Tags = dto.Tags,
                PublishedDate = DateTime.UtcNow
            };

            if (dto.Image != null)
            {
                blog.ImageUrl = await SaveImageAsync(dto.Image);
            }

            context.Blogs.Add(blog);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(BlogUpdateDto dto)
    {
        try
        {
            var blog = await context.Blogs.FindAsync(dto.Id);
            if (blog == null) return false;

            blog.Title = dto.Title;
            blog.Description = dto.Description;
            blog.Content = dto.Content;
            blog.Author = dto.Author;
            blog.Category = dto.Category;
            blog.Tags = dto.Tags;

            if (dto.Image != null)
            {
                // Delete old image
                if (!string.IsNullOrEmpty(blog.ImageUrl))
                {
                    DeleteImage(blog.ImageUrl);
                }
                blog.ImageUrl = await SaveImageAsync(dto.Image);
            }

            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var blog = await context.Blogs.FindAsync(id);
            if (blog == null) return false;

            // Delete image
            if (!string.IsNullOrEmpty(blog.ImageUrl))
            {
                DeleteImage(blog.ImageUrl);
            }

            context.Blogs.Remove(blog);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<BlogDto>> GetRecentBlogsAsync(int count)
    {
        return await context.Blogs
            .OrderByDescending(b => b.PublishedDate)
            .Take(count)
            .Select(b => new BlogDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Content = b.Content,
                ImageUrl = b.ImageUrl,
                Author = b.Author,
                PublishedDate = b.PublishedDate,
                ViewCount = b.ViewCount,
                Category = b.Category,
                Tags = b.Tags
            })
            .ToListAsync();
    }

    public async Task<List<BlogDto>> GetBlogsByCategoryAsync(string category)
    {
        return await context.Blogs
            .Where(b => b.Category == category)
            .OrderByDescending(b => b.PublishedDate)
            .Select(b => new BlogDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Content = b.Content,
                ImageUrl = b.ImageUrl,
                Author = b.Author,
                PublishedDate = b.PublishedDate,
                ViewCount = b.ViewCount,
                Category = b.Category,
                Tags = b.Tags
            })
            .ToListAsync();
    }

    public async Task IncrementViewCountAsync(int id)
    {
        var blog = await context.Blogs.FindAsync(id);
        if (blog != null)
        {
            blog.ViewCount++;
            await context.SaveChangesAsync();
        }
    }

    private async Task<string> SaveImageAsync(Microsoft.AspNetCore.Http.IFormFile file)
    {
        // Validate file
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is required");

        // Check file size (max 5MB)
        const long maxFileSize = 5 * 1024 * 1024; // 5MB
        if (file.Length > maxFileSize)
            throw new ArgumentException("File size cannot exceed 5MB");

        // Validate file type
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(fileExtension))
            throw new ArgumentException("Only image files are allowed");

        var fileName = Guid.NewGuid() + fileExtension;
        var uploadsFolder = Path.Combine(env.WebRootPath, "assets", "img", "blog");
        
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, fileName);
        
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/assets/img/blog/{fileName}";
    }

    private void DeleteImage(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl)) return;

        var filePath = Path.Combine(env.WebRootPath, imageUrl.TrimStart('/'));
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}

