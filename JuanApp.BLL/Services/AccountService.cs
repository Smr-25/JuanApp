using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JuanApp.BLL.Services;

public class AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : IAccountService
{
    public async Task<IdentityResult> RegisterAsync(AccountRegisterDto accountRegisterDto)
    {
        
       var user = new AppUser
       {
           FullName = accountRegisterDto.FullName,
           Email = accountRegisterDto.Email
       };
       var existingUser = await userManager.FindByEmailAsync(accountRegisterDto.Email);
       if (existingUser != null)
       {
           return IdentityResult.Failed(new IdentityError { Description = "Email is already in use." });
       }
       var result = await userManager.CreateAsync(user, accountRegisterDto.Password); 
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
        var result = await signInManager.PasswordSignInAsync(accountLoginDto.UsernameOrEmail, accountLoginDto.Password, accountLoginDto.RememberMe, false);
        if (!result.IsLockedOut)
        {
            return SignInResult.Failed;
        }
        return result;
    }
   
    
    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}