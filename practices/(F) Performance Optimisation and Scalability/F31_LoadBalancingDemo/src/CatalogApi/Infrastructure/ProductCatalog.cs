using CatalogApi.Models;

namespace CatalogApi.Infrastructure;

public sealed class ProductCatalog
{
    private readonly IReadOnlyList<Product> _products =
    [
        new(1, "Wireless Mouse", 24.99m),
        new(2, "Mechanical Keyboard", 89.99m),
        new(3, "USB-C Dock", 59.99m),
        new(4, "Noise Cancelling Headphones", 149.99m)
    ];

    public IReadOnlyList<Product> GetAll() => _products;

    public Product? GetById(int id) => _products.SingleOrDefault(p => p.Id == id);
}