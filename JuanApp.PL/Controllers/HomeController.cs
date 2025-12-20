using JuanApp.BLL.Interfaces;
using JuanApp.BLL.Services;
using JuanApp.DLL.Data;
using JuanApp.PL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.Controllers;

public class HomeController(ISliderService sliderService,IProductService productService,IAdvantagesService advantageService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var homeVm = new HomeVm
        {
            Slider = await sliderService.GetAllSlidersAsync(),
            Product = await productService.GetAllProductsAsync(),
            Advantage = await advantageService.GetAllAdvantagesAsync()
        };
        return View(homeVm);
    }


}
