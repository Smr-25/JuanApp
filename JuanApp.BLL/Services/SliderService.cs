using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class SliderService(AppDbContext db) : ISliderService
{
    public async Task<HomeSliderDto> GetAllSlidersAsync()
    {
        var mainSliders = await db.Sliders.Where(s => s.IsMain).ToListAsync();
        var sliders = await db.Sliders.Where(s => !s.IsMain).ToListAsync();
        return new HomeSliderDto
        {
            MainSliders = mainSliders,
            Sliders = sliders
        };
    }
}
