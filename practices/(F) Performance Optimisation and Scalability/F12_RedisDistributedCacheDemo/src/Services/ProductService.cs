using RedisDistributedCacheDemo.Models;
using RedisDistributedCacheDemo.Data;

namespace RedisDistributedCacheDemo.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo) => _repo = repo;

    public Task<Product?> GetProductAsync(Guid id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(CancellationToken ct = default)
        => _repo.GetFeaturedAsync(ct);

    public Task InvalidateProductAsync(Guid id, CancellationToken ct = default) => Task.CompletedTask;
    public Task InvalidateFeaturedAsync(CancellationToken ct = default) => Task.CompletedTask;
}