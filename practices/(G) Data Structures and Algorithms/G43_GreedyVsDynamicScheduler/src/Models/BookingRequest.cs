namespace GreedyVsDynamicScheduler.Models;

public sealed record BookingRequest(
    string Name,
    int StartHour,
    int EndHour,
    int Value
)
{
    public int Duration => EndHour - StartHour;
}