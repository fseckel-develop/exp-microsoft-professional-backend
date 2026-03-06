/*
HOW TO USE THIS DEMO

1. Start two CatalogApi instances in separate terminals.

   Terminal 1:
   >> ASPNETCORE_URLS=http://localhost:5001 InstanceName=catalog-api-1 dotnet run

   Terminal 2:
   >> ASPNETCORE_URLS=http://localhost:5002 InstanceName=catalog-api-2 dotnet run

2. Start this Gateway project.

   >> dotnet run

3. Send requests to the gateway instead of directly to the backend instances.

   Example:
   http://localhost:8080/api/products/1

4. Repeating the same request should alternate between backend instances because
   the configured YARP load balancing policy is RoundRobin.

5. You can verify which backend handled a request by looking at the "instance"
   field in the response payload.
*/

using Gateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<GatewayRouteInfoProvider>();

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapControllers();
app.MapReverseProxy();

app.Run("http://localhost:8080");