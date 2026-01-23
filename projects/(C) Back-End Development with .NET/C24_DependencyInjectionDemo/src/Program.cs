
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IMyService, SpecificService>();    // One Service Instance for all Requests
// builder.Services.AddScoped<IMyService, SpecificService>();    // One Service Instance per Request
// builder.Services.AddTransient<IMyService, SpecificService>(); // One Service Instance per Request Step (Middleware as well as Endpoint)
var app = builder.Build();



app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    myService.Log("Middleware (1) pre-processing");
    await next.Invoke();
    myService.Log("Middleware (1) post-processing");
});

app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    myService.Log("Middleware (2) pre-processing");
    await next.Invoke();
    myService.Log("Middleware (2) post-processing");
});


app.MapGet("/", (IMyService myService) => {
    myService.Log("Root");
    return Results.Ok("Check the console for the service creation log.");
});

app.Run();



public interface IMyService
{
    void Log(string message);
}


public class SpecificService : IMyService
{
    private readonly int _serviceId;

    public SpecificService()
    {
        _serviceId = new Random().Next(100000, 999999);
    }

    public void Log(string message)
    {
        Console.WriteLine($"[Service ID {_serviceId}] - {message}");
    }
}
