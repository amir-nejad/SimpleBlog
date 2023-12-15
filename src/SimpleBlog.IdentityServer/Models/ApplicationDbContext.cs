using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.IdentityServer.Utilities;

namespace SimpleBlog.IdentityServer.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole[]
            {
                new()
                {
                    Id = "9c9fb092-df93-4b41-8d81-3de2341d6586",
                    Name = CustomRoles.Administrator,
                    NormalizedName = CustomRoles.Administrator.ToUpper()
                }
            });
        }
    }
}