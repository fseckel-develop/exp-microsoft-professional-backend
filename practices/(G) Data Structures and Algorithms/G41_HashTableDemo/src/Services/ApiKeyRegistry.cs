using HashTableDemo.Collections;
using HashTableDemo.Models;

namespace HashTableDemo.Services;

public sealed class ApiKeyRegistry
{
    private readonly HashTable<string, ApiClient> _clientsByApiKey;

    public ApiKeyRegistry(int capacity = 11)
    {
        _clientsByApiKey = new HashTable<string, ApiClient>(capacity);
    }

    public void RegisterOrUpdate(string apiKey, ApiClient client)
    {
        _clientsByApiKey.AddOrUpdate(apiKey, client);
    }

    public bool Revoke(string apiKey)
    {
        return _clientsByApiKey.Remove(apiKey);
    }

    public bool TryGetClient(string apiKey, out ApiClient? client)
    {
        return _clientsByApiKey.TryGetValue(apiKey, out client);
    }

    public bool ContainsApiKey(string apiKey)
    {
        return _clientsByApiKey.ContainsKey(apiKey);
    }

    public int Count => _clientsByApiKey.Count;

    public IEnumerable<(int BucketIndex, IReadOnlyList<KeyValuePair<string, ApiClient>> Entries)> GetBucketSnapshot()
    {
        return _clientsByApiKey.GetBucketSnapshot();
    }
}