using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Controllers;

[ApiController]
[Route("api/home")]
public sealed class HomeController(IHomeService homeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var home = await homeService.GetHomeAsync();
        return Ok(home);
    }
}

