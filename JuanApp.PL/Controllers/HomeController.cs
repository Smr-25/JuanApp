using JuanApp.BLL.Interfaces;
using JuanApp.BLL.Services;
using JuanApp.DLL.Data;
using JuanApp.PL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.Controllers;

public class HomeController : Controller
{
    private readonly ISliderService _sliderService;
    private readonly IProductService _productService;
    private readonly IAdvantagesService _advantageService;
    private readonly AppDbContext _context;

    public HomeController(
        ISliderService sliderService,
        IProductService productService,
        IAdvantagesService advantageService,
        AppDbContext context)
    {
        _sliderService = sliderService;
        _productService = productService;
        _advantageService = advantageService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var homeVm = new HomeVm
        {
            Slider = await _sliderService.GetAllSlidersAsync(),
            Product = await _productService.GetAllProductsAsync(),
            Advantage = await _advantageService.GetAllAdvantagesAsync()
        };

        // Blog-ları ViewBag-ə əlavə edirik
        ViewBag.Blogs = await _context.Blogs
            .OrderByDescending(b => b.PublishedDate)
            .Take(3)
            .ToListAsync();

        return View(homeVm);
    }

}
