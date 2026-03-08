namespace SearchingAlgorithmsDemo.Benchmarking;

public sealed class SearchScenario<T, TKey>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required T[] Dataset { get; init; }
    public required TKey TargetKey { get; init; }
    public required Func<T, TKey> KeySelector { get; init; }
    public required IComparer<TKey> Comparer { get; init; }
    public required Func<T, string> Summarize { get; init; }
}