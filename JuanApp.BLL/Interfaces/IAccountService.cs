using JuanApp.BLL.Dtos;
using Microsoft.AspNetCore.Identity;

namespace JuanApp.BLL.Interfaces;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(AccountRegisterDto accountRegisterDto);
    Task<SignInResult> LoginAsync(AccountLoginDto accountLoginDto);
    Task LogoutAsync();
}