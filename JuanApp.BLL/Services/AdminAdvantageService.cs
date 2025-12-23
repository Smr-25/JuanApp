using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Domain.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class AdminAdvantageService(AppDbContext context) : IAdminAdvantageService
{
    public async Task<List<HomeAdvantageDto>> GetAllAdvantagesAsync()
    {
        var advantages = await context.Advantages.ToListAsync();
        return advantages.Select(a => new HomeAdvantageDto
        {
            Id = a.Id,
            Icon = a.Icon,
            Name = a.Name,
            Description = a.Description
        }).ToList();
    }

    public async Task<HomeAdvantageDto?> GetAdvantageByIdAsync(int id)
    {
        var advantage = await context.Advantages.FindAsync(id);
        if (advantage == null) return null;

        return new HomeAdvantageDto
        {
            Id = advantage.Id,
            Icon = advantage.Icon,
            Name = advantage.Name,
            Description = advantage.Description
        };
    }

    public async Task<bool> CreateAdvantageAsync(HomeAdvantageDto dto)
    {
        var advantage = new Advantage
        {
            Icon = dto.Icon,
            Name = dto.Name,
            Description = dto.Description
        };

        context.Advantages.Add(advantage);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAdvantageAsync(int id, HomeAdvantageDto dto)
    {
        var advantage = await context.Advantages.FindAsync(id);
        if (advantage == null) return false;

        advantage.Icon = dto.Icon;
        advantage.Name = dto.Name;
        advantage.Description = dto.Description;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAdvantageAsync(int id)
    {
        var advantage = await context.Advantages.FindAsync(id);
        if (advantage == null) return false;

        context.Advantages.Remove(advantage);
        await context.SaveChangesAsync();
        return true;
    }
}

