using JuanApp.Domain.Models;

namespace JuanApp.PL.ViewModel;

public class HomeVm
{
    public List<Slider> Sliders { get; set; }
    public List<Service> Services { get; set; }
    public List<Product> Products { get; set; }
}