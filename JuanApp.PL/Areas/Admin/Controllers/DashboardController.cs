using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

