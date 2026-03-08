using GreedyVsDynamicScheduler.Models;

namespace GreedyVsDynamicScheduler.Algorithms;

public sealed class DynamicProgrammingBookingScheduler : IScheduler
{
    public string Name => "Dynamic Programming (Optimal)";

    public SchedulingResult BuildSchedule(IReadOnlyList<BookingRequest> requests)
    {
        var sorted = requests
            .OrderBy(x => x.EndHour)
            .ThenBy(x => x.StartHour)
            .ToList();

        if (sorted.Count == 0)
        {
            return new SchedulingResult(Name, Array.Empty<BookingRequest>(), 0);
        }

        int[] previousCompatible = BuildPreviousCompatibleIndexMap(sorted);
        int[] bestValues = BuildBestValueTable(sorted, previousCompatible);
        var selected = ReconstructSolution(sorted, previousCompatible, bestValues);

        return new SchedulingResult(
            Algorithm: Name,
            SelectedBookings: selected,
            TotalValue: bestValues[^1]);
    }

    private static int[] BuildPreviousCompatibleIndexMap(IReadOnlyList<BookingRequest> requests)
    {
        var result = new int[requests.Count];

        for (int i = 0; i < requests.Count; i++)
        {
            result[i] = FindLastNonOverlapping(requests, i);
        }

        return result;
    }

    private static int[] BuildBestValueTable(
        IReadOnlyList<BookingRequest> requests,
        IReadOnlyList<int> previousCompatible)
    {
        var best = new int[requests.Count];

        for (int i = 0; i < requests.Count; i++)
        {
            int includeValue = requests[i].Value;

            if (previousCompatible[i] >= 0)
            {
                includeValue += best[previousCompatible[i]];
            }

            int excludeValue = i > 0 ? best[i - 1] : 0;

            best[i] = Math.Max(includeValue, excludeValue);
        }

        return best;
    }

    private static List<BookingRequest> ReconstructSolution(
        IReadOnlyList<BookingRequest> requests,
        IReadOnlyList<int> previousCompatible,
        IReadOnlyList<int> bestValues)
    {
        var result = new List<BookingRequest>();
        int index = requests.Count - 1;

        while (index >= 0)
        {
            int includeValue = requests[index].Value;

            if (previousCompatible[index] >= 0)
            {
                includeValue += bestValues[previousCompatible[index]];
            }

            int excludeValue = index > 0 ? bestValues[index - 1] : 0;

            if (includeValue >= excludeValue)
            {
                result.Add(requests[index]);
                index = previousCompatible[index];
            }
            else
            {
                index--;
            }
        }

        result.Reverse();
        return result;
    }

    private static int FindLastNonOverlapping(
        IReadOnlyList<BookingRequest> requests,
        int currentIndex)
    {
        int low = 0;
        int high = currentIndex - 1;

        while (low <= high)
        {
            int middle = low + (high - low) / 2;

            if (requests[middle].EndHour <= requests[currentIndex].StartHour)
            {
                if (middle + 1 <= high &&
                    requests[middle + 1].EndHour <= requests[currentIndex].StartHour)
                {
                    low = middle + 1;
                }
                else
                {
                    return middle;
                }
            }
            else
            {
                high = middle - 1;
            }
        }

        return -1;
    }
}