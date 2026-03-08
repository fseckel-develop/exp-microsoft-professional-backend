namespace GreedyVsDynamicScheduler.Models;

public sealed record SchedulingResult(
    string Algorithm,
    IReadOnlyList<BookingRequest> SelectedBookings,
    int TotalValue
);