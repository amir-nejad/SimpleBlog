
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleBlog.WebApi.Utilities;
using SimpleBlog.Application;
using SimpleBlog.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SimpleBlog.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            var identityServerUrl = builder.Configuration.GetValue<string>("IdentityServer:Url");

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = identityServerUrl;
                    options.Audience = "webApi";

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(ConfigConstants.RequireApiScope, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "webApi");
                });

                options.AddPolicy(ConfigConstants.RequireAdministratorRole, policy =>
                {
                    policy.RequireRole(CustomRoles.Administrator);
                });
            });

            builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple Blog Api", Version = "v1" });
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{identityServerUrl}/connect/authorize"),
                        TokenUrl = new Uri($"{identityServerUrl}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "webApi", "Access to Simple Blog API" },
                        },
                    }
                }
            });
            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "oauth2",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>
                        {
                            "webApi",
                            "role"
                        }
                    }
                });
        });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseCors(options =>
            //{
            //    options.AllowAnyOrigin();
            //    options.AllowCredentials();
            //    options.AllowAnyHeader();
            //    options.WithMethods("PUT", "GET", "POST", "DELETE");
            //});

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
