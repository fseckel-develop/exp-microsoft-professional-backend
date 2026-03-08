using System.Collections.Generic;

namespace HashTableDemo.Collections;

public sealed class HashTable<TKey, TValue> where TKey : notnull
{
    private readonly LinkedList<KeyValuePair<TKey, TValue>>[] _buckets;
    private readonly EqualityComparer<TKey> _keyComparer = EqualityComparer<TKey>.Default;

    public int Count { get; private set; }

    public HashTable(int capacity = 11)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];

        for (int i = 0; i < _buckets.Length; i++)
        {
            _buckets[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
        }
    }

    public void AddOrUpdate(TKey key, TValue value)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];

        var node = bucket.First;
        while (node is not null)
        {
            if (_keyComparer.Equals(node.Value.Key, key))
            {
                node.Value = new KeyValuePair<TKey, TValue>(key, value);
                return;
            }

            node = node.Next;
        }

        bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
        Count++;
    }

    public TValue Get(TKey key)
    {
        if (TryGetValue(key, out var value))
            return value!;

        throw new KeyNotFoundException($"Key '{key}' was not found.");
    }

    public bool TryGetValue(TKey key, out TValue? value)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];

        foreach (var pair in bucket)
        {
            if (_keyComparer.Equals(pair.Key, key))
            {
                value = pair.Value;
                return true;
            }
        }

        value = default;
        return false;
    }

    public bool ContainsKey(TKey key)
    {
        return TryGetValue(key, out _);
    }

    public bool Remove(TKey key)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];

        var node = bucket.First;
        while (node is not null)
        {
            if (_keyComparer.Equals(node.Value.Key, key))
            {
                bucket.Remove(node);
                Count--;
                return true;
            }

            node = node.Next;
        }

        return false;
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> GetEntries()
    {
        foreach (var bucket in _buckets)
        {
            foreach (var entry in bucket)
            {
                yield return entry;
            }
        }
    }

    public IEnumerable<(int BucketIndex, IReadOnlyList<KeyValuePair<TKey, TValue>> Entries)> GetBucketSnapshot()
    {
        for (int i = 0; i < _buckets.Length; i++)
        {
            yield return (i, _buckets[i].ToList());
        }
    }

    private int GetBucketIndex(TKey key)
    {
        int hashCode = _keyComparer.GetHashCode(key);
        uint normalized = (uint)hashCode;
        return (int)(normalized % (uint)_buckets.Length);
    }
}