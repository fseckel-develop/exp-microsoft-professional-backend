namespace SortingShowcaseDemo.Algorithms;

public sealed class BubbleSorter<T> : ISorter<T>
{
    public string Name => "Bubble Sort";

    public void Sort(T[] items, Comparison<T> comparison)
    {
        for (int pass = 0; pass < items.Length - 1; pass++)
        {
            bool swapped = false;

            for (int i = 0; i < items.Length - pass - 1; i++)
            {
                if (comparison(items[i], items[i + 1]) > 0)
                {
                    (items[i], items[i + 1]) = (items[i + 1], items[i]);
                    swapped = true;
                }
            }

            if (!swapped)
                break;
        }
    }
}