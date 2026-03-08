namespace SortingShowcaseDemo.Algorithms;

public interface ISorter<T>
{
    string Name { get; }
    void Sort(T[] items, Comparison<T> comparison);
}