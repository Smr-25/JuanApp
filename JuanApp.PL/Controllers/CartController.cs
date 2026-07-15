using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Controllers;

[ApiController]
[Route("api/cart")]
public sealed class CartController(ICartService cartService) : ControllerBase
{
    [HttpGet("mock")]
    public async Task<IActionResult> GetMock()
    {
        var cart = await cartService.GetMockCartAsync();
        return Ok(cart);
    }
}

