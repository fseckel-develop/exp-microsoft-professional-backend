using RedisDistributedCacheDemo.Models;

namespace RedisDistributedCacheDemo.Data;

public sealed class DemoProductRepository : IProductRepository
{
    private readonly List<Product> _data;

    public DemoProductRepository()
    {
        _data =
        [
            new Product(Guid.NewGuid(), "Keyboard", 79.99m, true),
            new Product(Guid.NewGuid(), "Mouse", 29.99m, true),
            new Product(Guid.NewGuid(), "Monitor", 219.00m, false),
        ];
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        await Task.Delay(150, token); // simulate I/O
        return _data.SingleOrDefault(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetFeaturedAsync(CancellationToken token = default)
    {
        await Task.Delay(250, token); // simulate I/O
        return _data.Where(p => p.IsFeatured).ToList();
    }

    // Helper for the demo runner
    public IReadOnlyList<Product> SeededData => _data;
}