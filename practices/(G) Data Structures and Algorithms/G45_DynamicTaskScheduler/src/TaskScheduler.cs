public class DynamicTaskScheduler
{
    private static List<Task> _tasks = new();
    private static Dictionary<int, int> _memo = new();

    public static List<Task> GetSchedule(List<Task> tasks)
    {
        _tasks = tasks.OrderBy(t => t.End).ToList();
        _memo = new Dictionary<int, int>();

        ComputeMaxValue(_tasks.Count - 1);

        return ReconstructSolution(_tasks.Count - 1);
    }

    private static int ComputeMaxValue(int taskIndex)
    {
        if (taskIndex < 0)
        {
            return 0;
        }

        if (_memo.ContainsKey(taskIndex))
        {
            return _memo[taskIndex];
        }

        int includeValue = _tasks[taskIndex].Value;
        int lastCompatible = FindLastNonOverlapping(taskIndex);

        includeValue += ComputeMaxValue(lastCompatible);

        int excludeValue = ComputeMaxValue(taskIndex - 1);

        int result = Math.Max(includeValue, excludeValue);

        _memo[taskIndex] = result;

        return result;
    }

    private static int FindLastNonOverlapping(int taskIndex)
    {
        int lowBound = 0;
        int highBound = taskIndex - 1;

        while (lowBound <= highBound)
        {
            int middle = (lowBound + highBound) / 2;
            if (_tasks[middle].End <= _tasks[taskIndex].Start)
            {
                if (middle + 1 <= highBound && _tasks[middle + 1].End <= _tasks[taskIndex].Start)
                {
                    lowBound = middle + 1;
                }
                else
                {
                    return middle;
                }
            }
            else
            {
                highBound = middle - 1;
            }
        }

        return -1;
    }

    private static List<Task> ReconstructSolution(int taskIndex)
    {
        List<Task> result = new();

        while (taskIndex >= 0)
        {
            int includeValue = _tasks[taskIndex].Value;
            int lastCompatible = FindLastNonOverlapping(taskIndex);
            includeValue += (lastCompatible >= 0) ? _memo[lastCompatible] : 0;

            int excludeValue = (taskIndex > 0) ? _memo[taskIndex - 1] : 0;

            if (includeValue >= excludeValue)
            {
                result.Add(_tasks[taskIndex]);
                taskIndex = lastCompatible;
            }
            else
            {
                taskIndex--;
            }
        }

        result.Reverse();
        return result;
    }
}