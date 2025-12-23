using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
public class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var stats = await _dashboardService.GetDashboardStatsAsync();
        ViewBag.ProductCount = stats.ProductCount;
        ViewBag.OrderCount = stats.OrderCount;
        ViewBag.SubscriberCount = stats.SubscriberCount;
        ViewBag.TotalRevenue = stats.TotalRevenue;

        ViewBag.RecentOrders = await _dashboardService.GetRecentOrdersAsync();
        ViewBag.PopularProducts = await _dashboardService.GetPopularProductsAsync();

        return View();
    }
}

