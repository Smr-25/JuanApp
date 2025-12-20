using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class AdvantageService(AppDbContext db) : IAdvantagesService
{
    public async Task<HomeAdvantageDto> GetAllAdvantagesAsync()
    {
        var advantages = await db.Advantages.ToListAsync();
        return new HomeAdvantageDto()
        {
            Advantages = advantages
        };
    }
}
