using JuanApp.BLL.Interfaces;
using JuanApp.PL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class HomeController : Controller
{
    private readonly ISliderService _sliderService;
    private readonly IProductService _productService;
    private readonly IAdvantagesService _advantageService;
    private readonly IBlogService _blogService;

    public HomeController(
        ISliderService sliderService,
        IProductService productService,
        IAdvantagesService advantageService,
        IBlogService blogService)
    {
        _sliderService = sliderService;
        _productService = productService;
        _advantageService = advantageService;
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        var homeVm = new HomeVm
        {
            Slider = await _sliderService.GetAllSlidersAsync(),
            Product = await _productService.GetAllProductsAsync(),
            Advantage = await _advantageService.GetAllAdvantagesAsync(),
            Blogs = await _blogService.GetRecentBlogsAsync(3)
        };

        return View(homeVm);
    }

}
