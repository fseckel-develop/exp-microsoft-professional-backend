using HashTableDemo.Models;

namespace HashTableDemo.Presentation;

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

    public void WriteClientRegistration(string apiKey, ApiClient client)
    {
        Console.WriteLine($"{apiKey,-18} -> {client.DisplayName,-18} Tier={client.Tier,-6} Id={client.ClientId}");
    }

    public void WriteAuthenticationResult(AuthenticationResult result)
    {
        if (result.IsAuthenticated && result.Client is not null)
        {
            Console.WriteLine(
                $"ALLOW  {result.Path,-20} key={result.ApiKey,-18} client={result.Client.DisplayName}");
        }
        else
        {
            Console.WriteLine(
                $"DENY   {result.Path,-20} key={result.ApiKey,-18} reason={result.Message}");
        }
    }

    public void WriteBooleanResult(string label, bool value)
    {
        Console.WriteLine($"{label,-30} {value}");
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteBucketSnapshot(
        IEnumerable<(int BucketIndex, IReadOnlyList<KeyValuePair<string, ApiClient>> Entries)> buckets)
    {
        Console.WriteLine("Bucket distribution:");
        foreach (var bucket in buckets)
        {
            var content = bucket.Entries.Count == 0
                ? "(empty)"
                : string.Join(" | ", bucket.Entries.Select(e => e.Key));

            Console.WriteLine($"Bucket {bucket.BucketIndex,2}: {content}");
        }

        Console.WriteLine();
    }
}