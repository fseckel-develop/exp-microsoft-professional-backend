public class GreedyTaskScheduler
{
    public static List<Task> GetSchedule(List<Task> tasks)
    {
        var sortedTasks = tasks.OrderByDescending(t => t.Value).ToList();

        var schedule = new List<Task>();

        foreach (var task in sortedTasks)
        {
            if (!Overlaps(task, schedule))
            {
                schedule.Add(task);
            }
        }

        return schedule.OrderBy(t => t.Start).ToList();
    }

    private static bool Overlaps(Task task, List<Task> scheduled)
    {
        foreach (var s in scheduled)
        {
            if (task.Start < s.End && task.End > s.Start)
                return true;
        }

        return false;
    }
}