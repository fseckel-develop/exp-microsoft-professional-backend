using InMemoryCacheDemo.Models;

namespace InMemoryCacheDemo.Services;

public interface IProductService
{
    Task<Product?> GetProductAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(CancellationToken ct = default);

    Task InvalidateProductAsync(Guid id, CancellationToken ct = default);
    Task InvalidateFeaturedAsync(CancellationToken ct = default);
}