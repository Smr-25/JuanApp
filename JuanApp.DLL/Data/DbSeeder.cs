using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace JuanApp.DLL.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        // Seed Roles
        string[] roleNames = { "SuperAdmin", "Admin", "Member" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Seed SuperAdmin User
        var superAdminEmail = "";
        var superAdminUser = await userManager.FindByEmailAsync(superAdminEmail);

        if (superAdminUser == null)
        {
            var adminUser = new AppUser
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                FullName = "",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "SuperAdmin");
            }
        }
        
        // Seed Regular Admin User
        var adminEmail = "";
        var adminUserCheck = await userManager.FindByEmailAsync(adminEmail);

        if (adminUserCheck == null)
        {
            var regularAdmin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Admin User",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(regularAdmin, "");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(regularAdmin, "Admin");
            }
        }
    }
}

