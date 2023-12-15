using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.IdentityServer.IdentityConfiguration;
using SimpleBlog.IdentityServer.Models;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlog.IdentityServer.Utilities;

namespace SimpleBlog.IdentityServer
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.EmitStaticAudienceClaim = true;
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityResourcesConfiguration.IdentityResources)
                .AddInMemoryApiScopes(ClientsConfiguration.ApiScopes)
                .AddInMemoryClients(ClientsConfiguration.Clients)
                .AddAspNetIdentity<ApplicationUser>();

            var userManager = builder.Services.BuildServiceProvider().GetRequiredService<UserManager<ApplicationUser>>();

            // Seed the default user only in development environment
            if (builder.Environment.IsDevelopment())
            {
                SeedData.SeedDefaultUser(userManager).Wait();
            }

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.MapRazorPages();

            return app;
        }
    }
}
