namespace AsyncAwaitDemo.Models;

public sealed record DashboardData(
    IReadOnlyList<Product> Products,
    IReadOnlyList<Review> Reviews
);