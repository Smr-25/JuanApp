using JuanApp.Domain.Models;

namespace JuanApp.BLL.Dtos;

public class HomeSliderDto
{
    public List<Slider> MainSliders { get; set; } = new();
    public List<Slider> Sliders { get; set; } = new();


}
