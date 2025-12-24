using JuanApp.BLL.Dtos;
using Microsoft.AspNetCore.Identity;

namespace JuanApp.BLL.Interfaces;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(AccountRegisterDto accountRegisterDto);
    Task<SignInResult> LoginAsync(AccountLoginDto accountLoginDto);
    Task LogoutAsync();
    Task<IdentityResult> ConfirmEmailAsync(string userEmail, string token);
    Task SendPasswordResetEmailAsync(string email);
    Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword, string confirmationPassword);
    Task<IdentityResult> ChangePasswordAsync(string email, string currentPassword, string newPassword);
}

