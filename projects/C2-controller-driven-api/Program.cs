
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();


// Information about controller-driven Endpoints
app.MapGet("/", () => 
{
    return "Controlled endpoints available at /controlled/products.";
});


app.MapControllers();
app.Run();
