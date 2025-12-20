using JuanApp.BLL.Interfaces;
using JuanApp.PL.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class AccountController(IAccountService accountService) : Controller
{
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterVm userRegisterVm)
    {
        if (!ModelState.IsValid)
            return View(userRegisterVm);

        var result = await accountService.RegisterAsync(userRegisterVm.accountRegisterDto);
        if (result.Succeeded)
        {
            return RedirectToAction("Login", "Account");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(userRegisterVm);
    }
    
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginVm userLoginVm)
    {
        if (!ModelState.IsValid)
            return View(userLoginVm);

        var result = await accountService.LoginAsync(userLoginVm.accountLoginDto);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(userLoginVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await accountService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}