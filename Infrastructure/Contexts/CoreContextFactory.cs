using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Contexts;

    /// <summary>
    /// Used by EF Core CLI (dotnet ef) to instantiate CoreContext at design time.
    /// </summary>
    public class CoreContextFactory : IDesignTimeDbContextFactory<CoreContext>
    {
        public CoreContext CreateDbContext(string[] args)
        {
            // Point at the folder containing your API project's appsettings.json
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "BookstoreManagementSystem");
    
            // Build a config builder that can read appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)                        // Requires Microsoft.Extensions.Configuration.FileExtensions
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
    
            // Pull your connection string by name
            var connectionString = configuration.GetConnectionString("CoreDatabase");
    
            // Create the DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<CoreContext>();
            optionsBuilder.UseSqlServer(connectionString);
    
            // Return a new CoreContext, wired up for SQL Server
            return new CoreContext(optionsBuilder.Options);
        }
    }
    

    // public class ApplicationUser : IdentityUser;
    //
    // public class AppDbContext : IdentityDbContext<ApplicationUser>
    // {
    //     public AppDbContext(DbContextOptions<AppDbContext> options)
    //         : base(options) { }
    // }
