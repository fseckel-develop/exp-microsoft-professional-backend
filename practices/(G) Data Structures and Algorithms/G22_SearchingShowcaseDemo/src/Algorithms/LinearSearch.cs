namespace SearchingAlgorithmsDemo.Algorithms;

public sealed class LinearSearch<T, TKey> : ISearchStrategy<T, TKey>
{
    public string Name => "Linear Search";

    public int FindIndex(T[] items, TKey key, Func<T, TKey> keySelector, IComparer<TKey> comparer)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (comparer.Compare(keySelector(items[i]), key) == 0)
                return i;
        }

        return -1;
    }
}