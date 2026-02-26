using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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

public class LogiTrackContextFactory : IDesignTimeDbContextFactory<LogiTrackContext>
{
    public LogiTrackContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<LogiTrackContext>();
        optionsBuilder.UseSqlite(connectionString);

        return new LogiTrackContext(optionsBuilder.Options);
    }
}
