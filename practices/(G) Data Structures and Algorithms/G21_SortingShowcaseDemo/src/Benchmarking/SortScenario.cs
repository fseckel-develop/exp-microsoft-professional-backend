namespace SortingShowcaseDemo.Benchmarking;

public sealed class SortScenario<T>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Comparison<T> Comparison { get; init; }
    public required Func<T, string> Summarize { get; init; }
}