using Microsoft.Extensions.Caching.Distributed;
using RedisDistributedCacheDemo.Data;
using RedisDistributedCacheDemo.Services;

/*
    Necessity: Redis
    Installation on Mac:
        1. brew install redis
        2. redis-server (manual startup)
           brew services start redis (as background service)
        3. redis-cli ping 
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "RedisApp:";
});

builder.Services.AddSingleton<IProductRepository, DemoProductRepository>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<IProductService>(sp =>
{
    var cache = sp.GetRequiredService<IDistributedCache>();
    var inner = sp.GetRequiredService<ProductService>();
    return new DistributedCachedProductService(cache, inner);
});

var app = builder.Build();

app.MapControllers();

app.Run();