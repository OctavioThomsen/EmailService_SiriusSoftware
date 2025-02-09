using Microsoft.AspNetCore.Identity;
using EmailService_SiriusSoftware.Models;

namespace EmailService_SiriusSoftware.Helpers
{
    public static class RoleHelper
    {
        public static async Task EnsureRolesAndAdminUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string roleName = "Admin";
            string adminUsername = "admin";
            string adminEmail = "admin@gmail.com";
            string adminPassword = "Admin123!";

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(adminUser, roleName))
                {
                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
        }
    }
}
