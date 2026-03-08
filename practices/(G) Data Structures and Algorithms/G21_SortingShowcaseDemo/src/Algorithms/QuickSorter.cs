namespace SortingShowcaseDemo.Algorithms;

public sealed class QuickSorter<T> : ISorter<T>
{
    public string Name => "Quick Sort";

    public void Sort(T[] items, Comparison<T> comparison)
    {
        if (items.Length <= 1)
            return;

        SortRange(items, 0, items.Length - 1, comparison);
    }

    private static void SortRange(T[] items, int low, int high, Comparison<T> comparison)
    {
        if (low >= high)
            return;

        int pivotIndex = Partition(items, low, high, comparison);

        SortRange(items, low, pivotIndex - 1, comparison);
        SortRange(items, pivotIndex + 1, high, comparison);
    }

    private static int Partition(T[] items, int low, int high, Comparison<T> comparison)
    {
        T pivot = items[high];
        int smallerBoundary = low - 1;

        for (int current = low; current < high; current++)
        {
            if (comparison(items[current], pivot) < 0)
            {
                smallerBoundary++;
                (items[smallerBoundary], items[current]) = (items[current], items[smallerBoundary]);
            }
        }

        (items[smallerBoundary + 1], items[high]) = (items[high], items[smallerBoundary + 1]);
        return smallerBoundary + 1;
    }
}