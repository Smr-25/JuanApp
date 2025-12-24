using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.ViewComponents;

public class LayoutSettingsViewComponent : ViewComponent
{
    private readonly ILayoutService _layoutService;

    public LayoutSettingsViewComponent(ILayoutService layoutService)
    {
        _layoutService = layoutService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var settings = await _layoutService.GetSettingsAsync();
        return View("Default", settings);
    }
}

