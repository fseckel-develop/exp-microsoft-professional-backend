using GreedyVsDynamicScheduler.Models;

namespace GreedyVsDynamicScheduler.Algorithms;

public sealed class GreedyBookingScheduler : IScheduler
{
    public string Name => "Greedy by Value";

    public SchedulingResult BuildSchedule(IReadOnlyList<BookingRequest> requests)
    {
        var sortedRequests = requests
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.EndHour)
            .ToList();

        var selected = new List<BookingRequest>();

        foreach (var request in sortedRequests)
        {
            if (!OverlapsAny(request, selected))
            {
                selected.Add(request);
            }
        }

        var orderedSchedule = selected
            .OrderBy(x => x.StartHour)
            .ThenBy(x => x.EndHour)
            .ToList();

        return new SchedulingResult(
            Algorithm: Name,
            SelectedBookings: orderedSchedule,
            TotalValue: orderedSchedule.Sum(x => x.Value));
    }

    private static bool OverlapsAny(
        BookingRequest candidate,
        IEnumerable<BookingRequest> scheduled)
    {
        foreach (var existing in scheduled)
        {
            if (candidate.StartHour < existing.EndHour &&
                candidate.EndHour > existing.StartHour)
            {
                return true;
            }
        }

        return false;
    }
}