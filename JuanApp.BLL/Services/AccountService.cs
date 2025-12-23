using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MimeKit.Text;

namespace JuanApp.BLL.Services;

public class AccountService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IEmailService emailService,
    ISubscriberService subscriberService) : IAccountService
{
    public async Task<IdentityResult> RegisterAsync(AccountRegisterDto accountRegisterDto)
    {
        var user = new AppUser
        {
            FullName = accountRegisterDto.FullName,
            Email = accountRegisterDto.Email,
            UserName = accountRegisterDto.Email  // ✅ Email istifadə et, FullName yox
        };
        
        var existingUser = await userManager.FindByEmailAsync(accountRegisterDto.Email);
        if (existingUser != null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Email is already in use." });
        }

        var result = await userManager.CreateAsync(user, accountRegisterDto.Password);
        
        // ✅ Yalnız user uğurla yaradıldıqdan sonra email göndər
        if (!result.Succeeded)
        {
            return result;
        }
        
        // Subscribe if user checked the box
        if (accountRegisterDto.Subscribe)
        {
            await subscriberService.SubscribeAsync(accountRegisterDto.Email);
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink =
            $"http://localhost:5119/Account/ConfirmEmail?userEmail={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

        try
        {
            FileStream fileStream = new FileStream("wwwroot/EmailTemplates/EmailConfirmation.html", FileMode.Open);
            using var streamReader = new StreamReader(fileStream);
            var emailBody = await streamReader.ReadToEndAsync();
            emailBody = emailBody.Replace("{{UserName}}", user.UserName);
            emailBody = emailBody.Replace("{{ConfirmationLink}}", confirmationLink);
            await emailService.SendEmailAsync(user.Email, "Confirm Your Email", emailBody);
        }
        catch (Exception ex)
        {
            // Email göndərilməsə belə, user yaradılıb - log et
            Console.WriteLine($"Email sending failed: {ex.Message}");
        }

        return result;
    }

    public async Task<SignInResult> LoginAsync(AccountLoginDto accountLoginDto)
    {
        // ✅ Əvvəlcə user-i tap
        var user = await userManager.FindByEmailAsync(accountLoginDto.UsernameOrEmail);
        if (user == null)
        {
            user = await userManager.FindByNameAsync(accountLoginDto.UsernameOrEmail);
            if (user == null)
            {
                return SignInResult.Failed;
            }
        }

        // ✅ Email confirmed yoxlamasını ƏN ƏVVƏL et
        if (!user.EmailConfirmed)
        {
            return SignInResult.Failed;
        }

        // ✅ User object ilə sign in et, username yox
        var result = await signInManager.PasswordSignInAsync(
            user,  // ✅ User object
            accountLoginDto.Password,
            accountLoginDto.RememberMe, 
            lockoutOnFailure: true);  // ✅ Lockout enable et

        return result;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string userEmail, string token)
    {
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        var result = await userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            await userManager.UpdateSecurityStampAsync(user);
        }

        return result;
    }

    public async Task SendPasswordResetEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
        {
            return;
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink =
            $"https://localhost:7062/Account/ResetPassword?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

        try
        {
            FileStream fileStream = new FileStream("wwwroot/EmailTemplates/PasswordReset.html", FileMode.Open);
            using var streamReader = new StreamReader(fileStream);
            var emailBody = await streamReader.ReadToEndAsync();
            emailBody = emailBody.Replace("{{UserName}}", user.UserName);
            emailBody = emailBody.Replace("{{ResetLink}}", resetLink);

            await emailService.SendEmailAsync(user.Email, "Reset Your Password", emailBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Password reset email failed: {ex.Message}");
        }
    }

    public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword,
        string confirmationPassword)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        // ✅ Bu yoxlama lazım deyil, ResetPasswordAsync özü yoxlayır
        var resetResult = await userManager.ResetPasswordAsync(user, token, newPassword);
        
        if (resetResult.Succeeded)
        {
            await userManager.UpdateSecurityStampAsync(user);
        }
        
        return resetResult;
    }
}