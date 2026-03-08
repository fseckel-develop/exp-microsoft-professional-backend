namespace SortingShowcaseDemo.Algorithms;

public sealed class MergeSorter<T> : ISorter<T>
{
    public string Name => "Merge Sort";

    public void Sort(T[] items, Comparison<T> comparison)
    {
        if (items.Length <= 1)
            return;

        var buffer = new T[items.Length];
        SortRange(items, buffer, 0, items.Length - 1, comparison);
    }

    private static void SortRange(T[] items, T[] buffer, int left, int right, Comparison<T> comparison)
    {
        if (left >= right)
            return;

        int middle = left + (right - left) / 2;

        SortRange(items, buffer, left, middle, comparison);
        SortRange(items, buffer, middle + 1, right, comparison);
        Merge(items, buffer, left, middle, right, comparison);
    }

    private static void Merge(T[] items, T[] buffer, int left, int middle, int right, Comparison<T> comparison)
    {
        int leftIndex = left;
        int rightIndex = middle + 1;
        int targetIndex = left;

        while (leftIndex <= middle && rightIndex <= right)
        {
            if (comparison(items[leftIndex], items[rightIndex]) <= 0)
            {
                buffer[targetIndex++] = items[leftIndex++];
            }
            else
            {
                buffer[targetIndex++] = items[rightIndex++];
            }
        }

        while (leftIndex <= middle)
            buffer[targetIndex++] = items[leftIndex++];

        while (rightIndex <= right)
            buffer[targetIndex++] = items[rightIndex++];

        for (int i = left; i <= right; i++)
            items[i] = buffer[i];
    }
}