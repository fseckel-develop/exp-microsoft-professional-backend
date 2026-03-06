using ExpirationPoliciesDemo.Presentation;
using ExpirationPoliciesDemo.Scenarios;

namespace ExpirationPoliciesDemo;

internal static class Program
{
    public static async Task Main()
    {
        var writer = new ConsoleWriter();

        var scenarios = new List<IScenario>
        {
            new AbsoluteExpirationScenario(writer),
            new SlidingExpirationScenario(writer),
            new DependentExpirationScenario(writer)
        };

        writer.Title("Expiration Policies Demo");

        foreach (var scenario in scenarios)
        {
            await scenario.RunAsync();
            writer.Separator();
        }

        writer.Info("Demo completed.");
    }
}