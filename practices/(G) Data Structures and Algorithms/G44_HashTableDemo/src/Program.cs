// File: Program.cs
using System;

class Program
{
    static void Main(string[] args)
    {
        HashTable<string, int> hashTable = new HashTable<string, int>();

        // Add key-value pairs
        hashTable.Add("Alice", 25);
        hashTable.Add("Bob", 30);
        hashTable.Add("Charlie", 35);

        // Retrieve values
        Console.WriteLine($"Alice's age:   {hashTable.Get("Alice")}");
        Console.WriteLine($"Bob's age:     {hashTable.Get("Bob")}");
        Console.WriteLine($"Charlie's age: {hashTable.Get("Charlie")}");

        // Update value for existing key
        hashTable.Add("Alice", 28);
        Console.WriteLine($"Alice's updated age: {hashTable.Get("Alice")}");

        // Check if keys exist
        Console.WriteLine($"Contains 'Bob'?   {hashTable.ContainsKey("Bob")}");
        Console.WriteLine($"Contains 'David'? {hashTable.ContainsKey("David")}");

        // Remove a key
        hashTable.Remove("Charlie");
        Console.WriteLine("Removed 'Charlie'");

        // Attempt to get removed key (will throw exception)
        try
        {
            Console.WriteLine(hashTable.Get("Charlie"));
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}