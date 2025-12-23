using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Domain.Models;
using JuanApp.DLL.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class AdminSliderService(AppDbContext context, IWebHostEnvironment env) : IAdminSliderService
{
    public async Task<List<HomeSliderDto>> GetAllSlidersAsync()
    {
        var sliders = await context.Sliders.ToListAsync();
        return sliders.Select(s => new HomeSliderDto
        {
            Id = s.Id,
            Title = s.Title,
            SubTitle = s.SubTitle,
            Description = s.Description,
            ImageUrl = s.ImageUrl,
            ButtonLink = s.ButtonLink,
            ButtonText = s.ButtonText,
            IsMain = s.IsMain
        }).ToList();
    }

    public async Task<HomeSliderDto?> GetSliderByIdAsync(int id)
    {
        var slider = await context.Sliders.FindAsync(id);
        if (slider == null) return null;

        return new HomeSliderDto
        {
            Id = slider.Id,
            Title = slider.Title,
            SubTitle = slider.SubTitle,
            Description = slider.Description,
            ImageUrl = slider.ImageUrl,
            ButtonLink = slider.ButtonLink,
            ButtonText = slider.ButtonText,
            IsMain = slider.IsMain
        };
    }

    public async Task<bool> CreateSliderAsync(HomeSliderDto dto, Microsoft.AspNetCore.Http.IFormFile? imageFile)
    {
        var slider = new Slider
        {
            Title = dto.Title,
            SubTitle = dto.SubTitle,
            Description = dto.Description,
            ButtonLink = dto.ButtonLink,
            ButtonText = dto.ButtonText,
            IsMain = dto.IsMain
        };

        if (imageFile != null)
        {
            slider.ImageUrl = await SaveFileAsync(imageFile);
        }

        context.Sliders.Add(slider);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateSliderAsync(int id, HomeSliderDto dto, Microsoft.AspNetCore.Http.IFormFile? imageFile)
    {
        var slider = await context.Sliders.FindAsync(id);
        if (slider == null) return false;

        slider.Title = dto.Title;
        slider.SubTitle = dto.SubTitle;
        slider.Description = dto.Description;
        slider.ButtonLink = dto.ButtonLink;
        slider.ButtonText = dto.ButtonText;
        slider.IsMain = dto.IsMain;

        if (imageFile != null)
        {
            if (!string.IsNullOrEmpty(slider.ImageUrl))
            {
                DeleteFile(slider.ImageUrl);
            }
            slider.ImageUrl = await SaveFileAsync(imageFile);
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteSliderAsync(int id)
    {
        var slider = await context.Sliders.FindAsync(id);
        if (slider == null) return false;

        if (!string.IsNullOrEmpty(slider.ImageUrl))
        {
            DeleteFile(slider.ImageUrl);
        }

        context.Sliders.Remove(slider);
        await context.SaveChangesAsync();
        return true;
    }

    private async Task<string> SaveFileAsync(Microsoft.AspNetCore.Http.IFormFile file)
    {
        var uploadsFolder = Path.Combine(env.WebRootPath, "uploads", "sliders");
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

        return "/uploads/sliders/" + uniqueFileName;
    }

    private void DeleteFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return;

        var fullPath = Path.Combine(env.WebRootPath, filePath.TrimStart('/'));
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}

