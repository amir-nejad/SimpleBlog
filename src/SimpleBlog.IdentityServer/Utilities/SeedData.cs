﻿using Microsoft.AspNetCore.Identity;
using SimpleBlog.IdentityServer.Models;

namespace SimpleBlog.IdentityServer.Utilities
{
    public static class SeedData
    {
        public static async Task SeedDefaultUser(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser { UserName = "admin", Email = "admin@amir-nejad.com" };
                await userManager.CreateAsync(user, "admin");

                // Optionally, assign roles to the default user
                await userManager.AddToRolesAsync(user, new[] { "Administrator" });
            }
        }
    }
}
