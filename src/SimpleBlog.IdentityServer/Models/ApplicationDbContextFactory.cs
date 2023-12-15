using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleBlog.IdentityServer.Models
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        //private readonly IConfiguration _configuration;

        //public ApplicationDbContextFactory(IConfiguration configuration)
        //{       
        //    _configuration = configuration;
        //}

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Build config
            //IConfiguration configuration = new ConfigurationBuilder()
            //    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../"))
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            //var connectionString = configuration.GetConnectionString("DefaultConnection");

            var connectionString = @"Server=.;Database=SimpleBlog.IdentityServerDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
