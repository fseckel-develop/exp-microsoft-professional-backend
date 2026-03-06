/*
HOW TO USE THIS DEMO

This project is designed to demonstrate load balancing across multiple instances
of the same backend service.
Run this CatalogApi project twice in two separate terminals with different ports
and different instance names.

Example:
Terminal 1:
>> ASPNETCORE_URLS=http://localhost:5001 InstanceName=catalog-api-1 dotnet run

Terminal 2:
>> ASPNETCORE_URLS=http://localhost:5002 InstanceName=catalog-api-2 dotnet run

Then start the Gateway project separately. The gateway forwards incoming requests
to these backend instances using YARP with round-robin load balancing.
When you call the gateway repeatedly, you should see the response alternate
between catalog-api-1 and catalog-api-2 via the "instance" field.
*/

using CatalogApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<InstanceContext>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var instanceName = configuration["InstanceName"] ?? "catalog-api-unknown";
    return new InstanceContext(instanceName);
});

builder.Services.AddSingleton<ProductCatalog>();

var app = builder.Build();

app.MapControllers();

app.Run();