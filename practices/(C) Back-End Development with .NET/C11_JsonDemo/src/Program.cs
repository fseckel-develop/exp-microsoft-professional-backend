using Newtonsoft.Json;

public class Product
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
}

public class Program
{
    public static void Main(string[] args)
    {
        var product = new Product
        {
            Name = "Laptop",
            Price = 999.99m,
            Tags = new List<string> { "Electronics", "Computers" }
        };

        // Serialize the product to JSON
        string jsonOutput = JsonConvert.SerializeObject(product, Formatting.Indented);
        System.Console.WriteLine("Serialized JSON:");
        System.Console.WriteLine(jsonOutput);

        // Deserialize the JSON back to a Product object
        string jsonInput = "{\"Name\":\"Smartphone\",\"Price\":799.99,\"Tags\":[\"Electronics\",\"Mobile\"]}";
        var deserializedProduct = JsonConvert.DeserializeObject<Product>(jsonInput)!;
        System.Console.WriteLine("\nDeserialized Product:");
        System.Console.WriteLine($"Name:  {deserializedProduct.Name}");
        System.Console.WriteLine($"Price: {deserializedProduct.Price}");
        System.Console.WriteLine($"Tags:  {string.Join(", ", deserializedProduct.Tags)}");
    }
}