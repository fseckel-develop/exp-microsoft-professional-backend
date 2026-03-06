using Microsoft.Extensions.Caching.Memory;
using InMemoryCacheDemo.Services;
using InMemoryCacheDemo.Data;
using InMemoryCacheDemo.Caching;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024;
});

builder.Services.AddSingleton<CacheStampede>();
builder.Services.AddSingleton<IProductRepository, DemoProductRepository>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<IProductService>(sp =>
{
    var cache = sp.GetRequiredService<IMemoryCache>();
    var inner = sp.GetRequiredService<ProductService>();
    var stampede = sp.GetRequiredService<CacheStampede>();

    return new InMemoryCachedProductService(cache, inner, stampede);
});

var app = builder.Build();

app.MapControllers();

app.Run();