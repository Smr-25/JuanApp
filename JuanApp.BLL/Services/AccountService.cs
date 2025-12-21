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
    IEmailService emailService) : IAccountService
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

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink =
            $"https://localhost:7062/Account/ConfirmEmail?userEmail={user.Email}&token={Uri.EscapeDataString(token)}";

        FileStream fileStream = new FileStream("wwwroot/EmailTemplates/EmailConfirmation.html", FileMode.Open);
        using var streamReader = new StreamReader(fileStream);
        var emailBody = await streamReader.ReadToEndAsync();
        emailBody = emailBody.Replace("{{username}}", user.UserName);
        emailBody = emailBody.Replace("{{ConfirmationLink}}", confirmationLink);

        await emailService.SendEmailAsync(user.Email, "Confirm Your Email", emailBody);


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

        var result = await signInManager.PasswordSignInAsync(accountLoginDto.UsernameOrEmail, accountLoginDto.Password,
            accountLoginDto.RememberMe, false);
        if (!user.EmailConfirmed)
        {
            return SignInResult.Failed;
        }

        if (result.IsLockedOut)
        {
            return SignInResult.Failed;
        }


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
}