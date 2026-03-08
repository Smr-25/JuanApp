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
            UserName = accountRegisterDto.FullName
        };
        
        var existingUser = await userManager.FindByEmailAsync(accountRegisterDto.Email);
        if (existingUser != null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Email is already in use." });
        }

        var result = await userManager.CreateAsync(user, accountRegisterDto.Password);
        
        if (!result.Succeeded)
        {
            return result;
        }
        
        if (accountRegisterDto.Subscribe)
        {
            await subscriberService.SubscribeAsync(accountRegisterDto.Email);
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink =
            $"http://localhost:5195/Account/ConfirmEmail?userEmail={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

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
            Console.WriteLine($"Email sending failed: {ex.Message}");
        }

        return result;
    }

    public async Task<SignInResult> LoginAsync(AccountLoginDto accountLoginDto)
    {
        var user = await userManager.FindByEmailAsync(accountLoginDto.UsernameOrEmail);
        if (user == null)
        {
            user = await userManager.FindByNameAsync(accountLoginDto.UsernameOrEmail);
            if (user == null)
            {
                return SignInResult.Failed;
            }
        }

        if (!user.EmailConfirmed)
        {
            return SignInResult.Failed;
        }

        var result = await signInManager.PasswordSignInAsync(
            user,
            accountLoginDto.Password,
            accountLoginDto.RememberMe, 
            lockoutOnFailure: true);

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
            $"http://localhost:5195/Account/ResetPassword?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

        try
        {
            FileStream fileStream = new FileStream("wwwroot/EmailTemplates/PasswordReset.html", FileMode.Open);
            using var streamReader = new StreamReader(fileStream);
            var emailBody = await streamReader.ReadToEndAsync();
            emailBody = emailBody.Replace("{{userName}}", user.UserName);
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

        var resetResult = await userManager.ResetPasswordAsync(user, token, newPassword);
        
        if (resetResult.Succeeded)
        {
            await userManager.UpdateSecurityStampAsync(user);
        }
        
        return resetResult;
    }

    public async Task<IdentityResult> ChangePasswordAsync(string email, string currentPassword, string newPassword)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        
        if (result.Succeeded)
        {
            await userManager.UpdateSecurityStampAsync(user);
        }

        return result;
    }
}

