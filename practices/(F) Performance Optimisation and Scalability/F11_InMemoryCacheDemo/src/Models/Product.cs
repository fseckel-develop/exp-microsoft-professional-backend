namespace InMemoryCacheDemo.Models;

public sealed record Product(
    Guid Id,
    string Name,
    decimal Price,
    bool IsFeatured
);