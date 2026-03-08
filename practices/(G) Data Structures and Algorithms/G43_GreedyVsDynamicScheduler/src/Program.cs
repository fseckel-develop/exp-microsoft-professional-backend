using GreedyVsDynamicScheduler.Algorithms;
using GreedyVsDynamicScheduler.Data;
using GreedyVsDynamicScheduler.Presentation;

namespace GreedyVsDynamicScheduler;

internal static class Program
{
    private static void Main()
    {
        var requests = BookingRequestFactory.CreateFreelancerDay();
        var writer = new ConsoleWriter();

        var greedyScheduler = new GreedyBookingScheduler();
        var dynamicScheduler = new DynamicProgrammingBookingScheduler();

        writer.WriteTitle("Greedy vs Dynamic Scheduling Demo");

        writer.WriteSection(
            "Available Booking Requests",
            "Each booking request has a time window and an associated payout.");

        writer.WriteBookings(requests);

        var greedyResult = greedyScheduler.BuildSchedule(requests);
        var dynamicResult = dynamicScheduler.BuildSchedule(requests);

        writer.WriteSection(
            "Greedy Scheduling",
            "Selects the highest-value non-overlapping bookings first.");

        writer.WriteSchedulingResult(greedyResult);

        writer.WriteSection(
            "Dynamic Programming Scheduling",
            "Computes the optimal weighted interval schedule.");

        writer.WriteSchedulingResult(dynamicResult);

        writer.WriteComparison(greedyResult, dynamicResult);
    }
}