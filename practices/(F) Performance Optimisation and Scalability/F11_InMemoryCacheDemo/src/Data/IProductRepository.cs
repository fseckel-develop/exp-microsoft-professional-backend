using InMemoryCacheDemo.Models;

namespace InMemoryCacheDemo.Data;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Product>> GetFeaturedAsync(CancellationToken ct = default);

    // demo helper
    IReadOnlyList<Product> SeededData { get; }
}