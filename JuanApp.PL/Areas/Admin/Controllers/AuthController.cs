using JuanApp.BLL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthController(
    SignInManager<AppUser> signInManager,
    UserManager<AppUser> userManager) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(AdminLoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View(dto);
        }

        // Check if user has admin role
        var roles = await userManager.GetRolesAsync(user);
        if (!roles.Contains("SuperAdmin") && !roles.Contains("Admin"))
        {
            ModelState.AddModelError("", "Access denied. Admin privileges required.");
            return View(dto);
        }

        var result = await signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError("", "Account locked. Try again later.");
            return View(dto);
        }

        ModelState.AddModelError("", "Invalid email or password");
        return View(dto);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}

