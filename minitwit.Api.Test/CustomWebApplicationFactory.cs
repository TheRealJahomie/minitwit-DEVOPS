using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpsPolicy;
using minitwit.Infrastructure.Data;


public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the PostgreSQL db context
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            services.Remove(dbContextDescriptor!);

            // Add SQLite instead of PostgreSQL
            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                options.UseSqlite(connection);
            });

            var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();  // Apply migrations
        });

        builder.UseEnvironment("Development");
    }
}