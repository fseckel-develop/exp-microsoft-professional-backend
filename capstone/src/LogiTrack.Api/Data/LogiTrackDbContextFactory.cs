using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LogiTrack.Api.Data;

/*
    To add Identity tables to the database, run:
        dotnet ef migrations add AddIdentity \
            --project src/LogiTrack.Api \
            --startup-project src/LogiTrack.Api \
            --output-dir Data/Migrations

    For migration run the following command from the project root directory:
        dotnet ef migrations add InitialCreate \
            --project src/LogiTrack.Api \
            --startup-project src/LogiTrack.Api \
            --output-dir Data/Migrations
    
    To apply the migration and create the database, run:
        dotnet ef database update \
            --project src/LogiTrack.Api \
            --startup-project src/LogiTrack.Api
*/

public class LogiTrackDbContextFactory : IDesignTimeDbContextFactory<LogiTrackDbContext>
{
    public LogiTrackDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<LogiTrackDbContext>();
        optionsBuilder.UseSqlite(connectionString);

        return new LogiTrackDbContext(optionsBuilder.Options);
    }
}