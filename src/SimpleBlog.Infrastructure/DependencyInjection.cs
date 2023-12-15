using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlog.Domain.Interfaces;
using SimpleBlog.Infrastructure.Data;
using SimpleBlog.Infrastructure.Models;

namespace SimpleBlog.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SimpleBlogContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }
    }
}
