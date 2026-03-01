using AsyncAwaitDemo.Models;

namespace AsyncAwaitDemo.Services;

public sealed class MarketplaceDataService
{
    public async Task<IReadOnlyList<Product>> FetchProductsAsync()
    {
        await Task.Delay(2000);

        return
        [
            new Product("Baked Camembert"),
            new Product("Pizza")
        ];
    }

    public async Task<IReadOnlyList<Review>> FetchReviewsAsync()
    {
        await Task.Delay(3000);

        return
        [
            new Review("Great product!"),
            new Review("Good value for the money.")
        ];
    }

    public async Task<DashboardData> LoadDashboardDataAsync()
    {
        var productsTask = FetchProductsAsync();
        var reviewsTask = FetchReviewsAsync();

        await Task.WhenAll(productsTask, reviewsTask);

        return new DashboardData(
            Products: await productsTask,
            Reviews: await reviewsTask
        );
    }
}