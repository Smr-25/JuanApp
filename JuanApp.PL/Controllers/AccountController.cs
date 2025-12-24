using System.Security.Claims;
using JuanApp.BLL.Interfaces;
using JuanApp.PL.ViewModel;
using JuanApp.PL.ViewModel.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ISubscriberService _subscriberService;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AccountController(
        IAccountService accountService, 
        ISubscriberService subscriberService,
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager)
    {
        _accountService = accountService;
        _subscriberService = subscriberService;
        _signInManager = signInManager;
        _userManager = userManager;
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

    [HttpPost]
    public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { success = false, message = "Please login first" });
        }

        if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
        {
            return Json(new { success = false, message = "All fields are required" });
        }

        if (newPassword != confirmPassword)
        {
            return Json(new { success = false, message = "New password and confirmation do not match" });
        }

        if (newPassword.Length < 6)
        {
            return Json(new { success = false, message = "Password must be at least 6 characters" });
        }

        var result = await _accountService.ChangePasswordAsync(User.Identity.Name, currentPassword, newPassword);
        
        if (result.Succeeded)
        {
            return Json(new { success = true, message = "Password changed successfully" });
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        return Json(new { success = false, message = errors });
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

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ExternalLogin(string provider, string returnUrl = null)
    {
        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
    {
        if (remoteError != null)
        {
            ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
            return RedirectToAction(nameof(Login));
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToAction(nameof(Login));
        }

        // Sign in the user with this external login provider if the user already has a login.
        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        
        if (result.Succeeded)
        {
            return RedirectToLocal(returnUrl);
        }

        if (result.IsLockedOut)
        {
            return RedirectToAction(nameof(Login));
        }
        else
        {
            // If the user does not have an account, then create one
            var email = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Email);
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                FullName = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Name),
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                createResult = await _userManager.AddLoginAsync(user, info);
                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }
            }

            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return RedirectToAction(nameof(Login));
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
}

