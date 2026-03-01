using AsyncAwaitDemo.Presentation;
using AsyncAwaitDemo.Services;

namespace AsyncAwaitDemo;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var writer = new ConsoleWriter();

        var fileImportService = new FileImportService(writer);
        var analyticsService = new AnalyticsProcessingService(writer);
        var marketplaceDataService = new MarketplaceDataService();
        var backgroundJobService = new BackgroundJobService(writer);

        writer.WriteTitle("Async Marketplace Demo");

        writer.WriteSection(
            "Concurrent File Imports",
            "Import multiple files concurrently using Task.WhenAll.");
        await fileImportService.ImportFilesAsync();

        writer.WriteSpacer();

        writer.WriteSection(
            "Parallel Batch Processing",
            "Process multiple analytics chunks asynchronously.");
        await analyticsService.ProcessLargeDatasetAsync(10);

        writer.WriteSpacer();

        writer.WriteSection(
            "Concurrent Dashboard Loading",
            "Load products and reviews concurrently before displaying dashboard data.");
        var dashboardData = await marketplaceDataService.LoadDashboardDataAsync();
        writer.WriteDashboardData(dashboardData);

        writer.WriteSpacer();

        writer.WriteSection(
            "Async Error Handling",
            "Handle exceptions from a long-running asynchronous background job.");
        await backgroundJobService.RunFailingJobAsync();

        writer.WriteMessage("Done.");
    }
}