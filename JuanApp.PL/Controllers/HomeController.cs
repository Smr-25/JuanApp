using JuanApp.DLL.Data;
using JuanApp.PL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.Controllers;

public class HomeController(AppDbContext db)  : Controller
{
    public IActionResult Index()
    { 
        var homeVm = new HomeVm
        {
            Sliders = db.Sliders.ToList(),
            Services = db.Services.ToList(),
            Products = db.Products.ToList()
        };

        return View(homeVm);
    }


}
