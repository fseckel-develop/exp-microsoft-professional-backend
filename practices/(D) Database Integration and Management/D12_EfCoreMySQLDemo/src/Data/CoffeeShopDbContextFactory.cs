using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EfCoreMySQLDemo.Data;

public sealed class CoffeeShopDbContextFactory : IDesignTimeDbContextFactory<CoffeeShopDbContext>
{
    public CoffeeShopDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connection = configuration.GetConnectionString("MySQL")
                   ?? throw new InvalidOperationException("Connection string 'MySQL' not found.");

        var options = new DbContextOptionsBuilder<CoffeeShopDbContext>()
            .UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 26)))
            .Options;

        return new CoffeeShopDbContext(options);
    }
}