using SimpleBlog.IdentityServer;
using Serilog;
using Microsoft.AspNetCore.Identity;
using SimpleBlog.IdentityServer.Models;
using SimpleBlog.IdentityServer.Utilities;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.UseCors(options =>
    {
        options.WithOrigins("https://localhost:7196");
        options.AllowCredentials();
        options.AllowAnyHeader();
        options.WithMethods("PUT", "GET", "POST", "DELETE");
    });

    using (var scope = app.Services.CreateScope())
    {
        var userManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>))!;

        SeedData.SeedDefaultUser(userManager).GetAwaiter().GetResult();
    }

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
