using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using SimpleBlog.IdentityServer.Models;
using System.Security.Claims;

namespace SimpleBlog.IdentityServer.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        Console.WriteLine("GetProfileDataAsync method called!");

        var userId = context.Subject;
        var claims = new List<Claim>
        {
            // other claims
        };

        // Include roles in the claims if the user has any roles
        if (userId.Identity!.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim("role", role)));
        }

        context.IssuedClaims.AddRange(claims);
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.CompletedTask;
    }

}