using JuanApp.BLL.Interfaces;
using JuanApp.PL.ViewModel;
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

    public async Task<IActionResult> Profile()
    {
        return View();
    }

    public async Task<IActionResult> ConfirmEmail(string userEmail, string token)
    {
        var result = await accountService.ConfirmEmailAsync(userEmail, token);
        if (result.Succeeded)
        {
            return RedirectToAction("Login", "Account");
        }

        return BadRequest("Email confirmation failed.");
    }

    public async Task<IActionResult> ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordVm forgotPasswordVm)
    {
        if (!ModelState.IsValid)
            return View(forgotPasswordVm);

        await accountService.SendPasswordResetEmailAsync(forgotPasswordVm.Email);
        return RedirectToAction("Login", "Account");
    }
    
    public async Task<IActionResult> ResetPassword(string email, string token)  // ✅ parametr sırası
    {
        var resetPasswordVm = new ResetPasswordVm
        {
            Token = token,
            Email = email
        };
        return View(resetPasswordVm);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordVm resetPasswordVm)
    {
        if (!ModelState.IsValid)
            return View(resetPasswordVm);
        
        var result = await accountService.ResetPasswordAsync(resetPasswordVm.Email, resetPasswordVm.Token,
            resetPasswordVm.Password, resetPasswordVm.ConfirmPassword);
        if (result.Succeeded)
        {
            return RedirectToAction("Login", "Account");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(resetPasswordVm);
            
           
    }
}