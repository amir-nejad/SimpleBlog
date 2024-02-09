using Microsoft.AspNetCore.Identity;
using SimpleBlog.IdentityServer.Models;

namespace SimpleBlog.IdentityServer.Utilities
{
    public static class SeedData
    {
        public static async Task SeedDefaultUser(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser { UserName = "admin@amir-nejad.com", Email = "admin@amir-nejad.com", EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, "Admin@123456");

                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, new[] { CustomRoles.Administrator });
                }
            }
        }
    }
}
