var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        message = "Controller-driven endpoints available at /controlled/products"
    });
});

app.MapControllers();

app.Run();