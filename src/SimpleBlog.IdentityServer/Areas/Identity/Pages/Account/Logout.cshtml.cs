// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SimpleBlog.IdentityServer.Models;

namespace SimpleBlog.IdentityServer.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public LogoutModel(
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger,
            IIdentityServerInteractionService interaction,
            IEventService events)
        {
            _signInManager = signInManager;
            _logger = logger;
            _interaction = interaction;
            _events = events;
        }

        public string LogoutId { get; set; }

        public void OnGet(string logoutId)
        {
            LogoutId = logoutId;    
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null, string logoutId = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {

                var context = await _interaction.GetLogoutContextAsync(logoutId);

                if (context?.PostLogoutRedirectUri != null)
                {
                    return Redirect(context.PostLogoutRedirectUri);
                }

                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
