using SortingShowcaseDemo.Algorithms;
using SortingShowcaseDemo.Benchmarking;
using SortingShowcaseDemo.Data;
using SortingShowcaseDemo.Presentation;

namespace SortingShowcaseDemo;

internal static class Program
{
    private static void Main()
    {
        const int productCount = 1_000_000;

        Product[] catalog = ProductFactory.CreateRandomCatalog(productCount);

        var scenario = new SortScenario<Product>
        {
            Name = "Products sorted by ascending price",
            Description = "Compare sorting algorithms on a product catalog ordered by price.",
            Comparison = (left, right) => left.Price.CompareTo(right.Price),
            Summarize = product => $"{product.Name} (${product.Price})"
        };

        var sorters = new ISorter<Product>[]
        {
            new QuickSorter<Product>(),
            new MergeSorter<Product>(),
            new BubbleSorter<Product>()
        };

        var benchmark = new SortBenchmark<Product>();
        var writer = new ConsoleReportWriter();

        writer.WriteIntro("Sorting Algorithms Showcase");
        writer.WriteScenarioHeader(scenario.Name, scenario.Description, productCount);

        foreach (var sorter in sorters)
        {
            var report = benchmark.Run(sorter, catalog, scenario);
            writer.WriteReport(report);
        }

        writer.WriteObservation(
            "- Bubble Sort is useful for learning but scales poorly.\n" +
            "- Quick Sort and Merge Sort are more suitable for realistic datasets.");
    }
}