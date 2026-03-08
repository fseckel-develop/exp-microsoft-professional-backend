using HashTableDemo.Data;
using HashTableDemo.Models;
using HashTableDemo.Presentation;
using HashTableDemo.Services;

namespace HashTableDemo;

internal static class Program
{
    private static void Main()
    {
        var writer = new ConsoleWriter();
        var registry = new ApiKeyRegistry(capacity: 7);
        var authenticator = new RequestAuthenticator(registry);

        writer.WriteTitle("Hash Table API Key Registry Demo");

        writer.WriteSection(
            "Client Registration",
            "Register API clients in an in-memory key index backed by a custom hash table.");

        foreach (var (apiKey, client) in ApiClientFactory.CreateSampleClients())
        {
            registry.RegisterOrUpdate(apiKey, client);
            writer.WriteClientRegistration(apiKey, client);
        }

        Console.WriteLine();

        writer.WriteSection(
            "Internal Bucket Distribution",
            "Inspect how entries are distributed across buckets in the hash table.");

        writer.WriteBucketSnapshot(registry.GetBucketSnapshot());

        writer.WriteSection(
            "Request Authentication",
            "Look up API keys to authorize incoming requests.");

        foreach (var request in ApiRequestFactory.CreateInitialRequests())
        {
            var result = authenticator.Authenticate(request);
            writer.WriteAuthenticationResult(result);
        }

        Console.WriteLine();

        writer.WriteSection(
            "Client Update",
            "Update an existing registration to simulate client profile changes.");

        registry.RegisterOrUpdate(
            "key_live_alpine",
            new ApiClient("client-001", "Alpine Analytics", "Platinum"));

        writer.WriteBooleanResult(
            "Contains key_live_alpine?",
            registry.ContainsApiKey("key_live_alpine"));

        writer.WriteAuthenticationResult(
            authenticator.Authenticate(new ApiRequest("key_live_alpine", "/v1/usage")));

        Console.WriteLine();

        writer.WriteSection(
            "Key Revocation",
            "Remove an API key from the registry so future requests fail authentication.");

        writer.WriteBooleanResult(
            "Revoked key_live_cobalt?",
            registry.Revoke("key_live_cobalt"));

        Console.WriteLine();

        writer.WriteSection(
            "Authentication After Revocation",
            "A revoked key should no longer resolve in the hash table.");

        writer.WriteAuthenticationResult(
            authenticator.Authenticate(new ApiRequest("key_live_cobalt", "/v1/patients")));

        Console.WriteLine();

        writer.WriteSection(
            "Final Bucket Distribution",
            "Inspect the hash table again after updates and revocation.");

        writer.WriteBucketSnapshot(registry.GetBucketSnapshot());
    }
}