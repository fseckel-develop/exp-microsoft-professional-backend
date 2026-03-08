namespace SearchingAlgorithmsDemo.Algorithms;

public sealed class BinarySearch<T, TKey> : ISearchStrategy<T, TKey>
{
    public string Name => "Binary Search";

    public int FindIndex(T[] items, TKey key, Func<T, TKey> keySelector, IComparer<TKey> comparer)
    {
        int left = 0;
        int right = items.Length - 1;

        while (left <= right)
        {
            int middle = left + (right - left) / 2;
            int comparison = comparer.Compare(keySelector(items[middle]), key);

            if (comparison == 0)
                return middle;

            if (comparison < 0)
                left = middle + 1;
            else
                right = middle - 1;
        }

        return -1;
    }
}