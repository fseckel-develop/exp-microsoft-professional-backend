using LogiTrack.Api.Data.Seed;
using LogiTrack.Api.Services;

namespace LogiTrack.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLogiTrackDatabase(builder.Configuration);
        builder.Services.AddLogiTrackCaching();
        builder.Services.AddLogiTrackIdentity();
        builder.Services.AddLogiTrackAuthentication(builder.Configuration);
        builder.Services.AddLogiTrackAuthorization();
        builder.Services.AddLogiTrackApplicationServices(builder.Configuration);
        builder.Services.AddLogiTrackSwagger();

        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
        }

        await using (var scope = app.Services.CreateAsyncScope())
        {
            await RoleSeeder.SeedAsync(scope.ServiceProvider);
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}