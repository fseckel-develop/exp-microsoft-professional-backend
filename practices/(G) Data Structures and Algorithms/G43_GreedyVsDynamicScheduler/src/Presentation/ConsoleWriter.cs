using GreedyVsDynamicScheduler.Models;

namespace GreedyVsDynamicScheduler.Presentation;

public sealed class ConsoleWriter
{
    public void WriteTitle(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('=', title.Length));
        Console.WriteLine();
    }

    public void WriteSection(string title, string description)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
        Console.WriteLine(description);
        Console.WriteLine();
    }

    public void WriteBookings(IEnumerable<BookingRequest> bookings)
    {
        Console.WriteLine($"{"Client",-12} {"Start",5} {"End",5} {"Value",7}");
        Console.WriteLine(new string('-', 32));

        foreach (var booking in bookings
             .OrderBy(x => x.StartHour)
             .ThenBy(x => x.EndHour))
        {
            Console.WriteLine(
                $"{booking.Name,-12} {booking.StartHour,5} {booking.EndHour,5} {booking.Value,7}");
        }

        Console.WriteLine();
    }

    public void WriteSchedulingResult(SchedulingResult result)
    {
        Console.WriteLine(result.Algorithm);
        Console.WriteLine(new string('-', result.Algorithm.Length));

        WriteBookings(result.SelectedBookings);
        
        Console.WriteLine($"Total value: {result.TotalValue}");
        Console.WriteLine();
    }

    public void WriteComparison(SchedulingResult greedy, SchedulingResult optimal)
    {
        Console.WriteLine("Comparison");
        Console.WriteLine("----------");
        Console.WriteLine($"Greedy total value:   {greedy.TotalValue}");
        Console.WriteLine($"Optimal total value:  {optimal.TotalValue}");
        Console.WriteLine($"Difference:           {optimal.TotalValue - greedy.TotalValue}");
        Console.WriteLine();

        if (greedy.TotalValue == optimal.TotalValue)
        {
            Console.WriteLine("For this dataset, the greedy strategy happened to match the optimal result.");
        }
        else
        {
            Console.WriteLine("The greedy strategy was simpler, but the dynamic programming approach found a better overall schedule.");
        }

        Console.WriteLine();
    }
}