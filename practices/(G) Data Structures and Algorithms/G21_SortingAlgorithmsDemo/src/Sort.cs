public class Sort
{
    public static void BubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }
        }
    }

    public static void QuickSort(int[] array, int lowBound, int highBound)
    {
        if (lowBound < highBound)
        {
            int pivot = Partition(array, lowBound, highBound);
            QuickSort(array, lowBound, pivot - 1);
            QuickSort(array, pivot + 1, highBound);
        }
    }

    private static int Partition(int[] array, int lowBound, int highBound)
    {
        int pivot = array[highBound];
        int i = lowBound - 1;
        for (int j = lowBound; j < highBound; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
        (array[i + 1], array[highBound]) = (array[highBound], array[i + 1]);
        return i + 1;
    }

    public static void MergeSort(int[] array, int leftBound, int rightBound)
    {
        if (leftBound < rightBound)
        {
            int middle = (leftBound + rightBound) / 2;
            MergeSort(array, leftBound, middle);
            MergeSort(array, middle + 1, rightBound);
            Merge(array, leftBound, middle, rightBound);
        }
    }

    private static void Merge(int[] array, int leftBound, int middle, int rightBound)
    {
        int[] leftArr = array[leftBound..(middle + 1)];
        int[] rightArr = array[(middle + 1)..(rightBound + 1)];
        int i = 0, j = 0, k = leftBound;
        while (i < leftArr.Length && j < rightArr.Length)
        {
            if (leftArr[i] <= rightArr[j])
                array[k++] = leftArr[i++];
            else
                array[k++] = rightArr[j++];
        }
        while (i < leftArr.Length) array[k++] = leftArr[i++];
        while (j < rightArr.Length) array[k++] = rightArr[j++];
    }
}