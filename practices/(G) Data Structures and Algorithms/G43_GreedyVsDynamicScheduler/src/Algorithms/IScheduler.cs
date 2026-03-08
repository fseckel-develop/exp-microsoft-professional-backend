using GreedyVsDynamicScheduler.Models;

namespace GreedyVsDynamicScheduler.Algorithms;

public interface IScheduler
{
    string Name { get; }
    SchedulingResult BuildSchedule(IReadOnlyList<BookingRequest> requests);
}