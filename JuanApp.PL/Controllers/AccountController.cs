using JuanApp.BLL.Interfaces;
using JuanApp.PL.ViewModel;
using JuanApp.PL.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ISubscriberService _subscriberService;

    public AccountController(IAccountService accountService, ISubscriberService subscriberService)
    {
        _accountService = accountService;
        _subscriberService = subscriberService;
    }

    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterVm userRegisterVm, bool Subscribe = false)
    {
        if (!ModelState.IsValid)
            return View(userRegisterVm);

        var result = await _accountService.RegisterAsync(userRegisterVm.accountRegisterDto);
        if (result.Succeeded)
        {
            // Subscribe if checkbox was checked
            if (Subscribe)
            {
                await _subscriberService.SubscribeAsync(userRegisterVm.accountRegisterDto.Email);
            }

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

        var result = await _accountService.LoginAsync(userLoginVm.accountLoginDto);
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
        await _accountService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Profile()
    {
        return View();
    }

    public async Task<IActionResult> ConfirmEmail(string userEmail, string token)
    {
        var result = await _accountService.ConfirmEmailAsync(userEmail, token);
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

        await _accountService.SendPasswordResetEmailAsync(forgotPasswordVm.Email);
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
        
        var result = await _accountService.ResetPasswordAsync(resetPasswordVm.Email, resetPasswordVm.Token,
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