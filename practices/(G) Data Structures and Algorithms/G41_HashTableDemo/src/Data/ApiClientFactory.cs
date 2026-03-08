using HashTableDemo.Models;

namespace HashTableDemo.Data;

public static class ApiClientFactory
{
    public static IReadOnlyList<(string ApiKey, ApiClient Client)> CreateSampleClients()
    {
        return
        [
            ("key_live_alpine", new ApiClient("client-001", "Alpine Analytics", "Gold")),
            ("key_live_beacon", new ApiClient("client-002", "Beacon Commerce", "Silver")),
            ("key_live_cobalt", new ApiClient("client-003", "Cobalt Health", "Gold"))
        ];
    }
}