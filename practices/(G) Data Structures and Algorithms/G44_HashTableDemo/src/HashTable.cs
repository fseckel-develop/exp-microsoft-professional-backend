public class HashTable<K, V>
{
    private const int Size = 10;
    private LinkedList<KeyValuePair<K, V>>[] buckets;

    public HashTable()
    {
        buckets = new LinkedList<KeyValuePair<K, V>>[Size];
        for (int i = 0; i < Size; i++)
        {
            buckets[i] = new LinkedList<KeyValuePair<K, V>>();
        }
    }

    private int GetBucketIndex(K key)
    {
        int hashCode = Math.Abs(key!.GetHashCode());
        return hashCode % Size;
    }

    public void Add(K key, V value)
    {
        int index = GetBucketIndex(key);
        var bucket = buckets[index];

        var node = bucket.First;
        while (node != null)
        {
            if (node.Value.Key!.Equals(key))
            {
                node.Value = new KeyValuePair<K, V>(key, value);
                return; // Key exists, value updated
            }
            node = node.Next;
        }

        bucket.AddLast(new KeyValuePair<K, V>(key, value));
    }

    public V Get(K key)
    {
        int index = GetBucketIndex(key);
        var bucket = buckets[index];

        foreach (var pair in bucket)
        {
            if (pair.Key!.Equals(key))
            {
                return pair.Value;
            }
        }

        throw new KeyNotFoundException($"Key '{key}' not found.");
    }

    public bool Remove(K key)
    {
        int index = GetBucketIndex(key);
        var bucket = buckets[index];

        var node = bucket.First;
        while (node != null)
        {
            if (node.Value.Key!.Equals(key))
            {
                bucket.Remove(node);
                return true;
            }
            node = node.Next;
        }

        return false; // Key not found
    }

    public bool ContainsKey(K key)
    {
        int index = GetBucketIndex(key);
        var bucket = buckets[index];

        foreach (var pair in bucket)
        {
            if (pair.Key!.Equals(key))
            {
                return true;
            }
        }

        return false;
    }
}