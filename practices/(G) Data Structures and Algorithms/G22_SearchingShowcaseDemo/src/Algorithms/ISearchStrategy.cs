namespace SearchingAlgorithmsDemo.Algorithms;

public interface ISearchStrategy<T, TKey>
{
    string Name { get; }
    int FindIndex(T[] items, TKey key, Func<T, TKey> keySelector, IComparer<TKey> comparer);
}