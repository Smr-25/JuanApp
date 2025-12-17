using JuanApp.DLL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.Controllers;

public class HomeController(AppDbContext db)  : Controller
{
    public IActionResult Index()
    {
        var sliders = db.Sliders.ToList();

        return View(sliders);
    }


}
