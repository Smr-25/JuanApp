using JuanApp.BLL.Dtos;
using JuanApp.Domain.Models;

namespace JuanApp.PL.ViewModel;

public class HomeVm
{
    public HomeSliderDto Slider { get; set; }
    public HomeAdvantageDto Advantage { get; set; }
    public HomeProductDto Product { get; set; }
}