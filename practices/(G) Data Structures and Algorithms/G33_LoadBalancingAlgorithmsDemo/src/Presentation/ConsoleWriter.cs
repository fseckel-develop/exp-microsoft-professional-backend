using LoadBalancingAlgorithmsDemo.Models;

namespace LoadBalancingAlgorithmsDemo.Presentation;

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

    public void WriteRoutingResult(RoutingResult result)
    {
        Console.WriteLine(
            $"{result.ClientIp,-15} {result.Path,-25} -> {result.BackendId}");
    }

    public void WriteBackendSummary(IEnumerable<BackendInstance> backends)
    {
        Console.WriteLine();
        Console.WriteLine("Backend summary:");
        foreach (var backend in backends)
        {
            Console.WriteLine(
                $"{backend.Id,-15} Weight={backend.Weight}, ActiveConnections={backend.ActiveConnections}, TotalRequests={backend.TotalRequestsHandled}");
        }
    }

    public void WriteSpacer()
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 70));
        Console.WriteLine();
    }
}