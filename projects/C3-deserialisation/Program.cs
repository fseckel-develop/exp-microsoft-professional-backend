using System.Text.Json;
using System.Xml.Serialization;



var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



// DESERIALIZATION EXAMPLES:

// Manual JSON deserialization:
app.MapPost("/manual-json", async (HttpContext context) => {
    var personFromClient = await context.Request.ReadFromJsonAsync<Person>();
    return TypedResults.Json(personFromClient);
});

// Custom JSON deserialization:
app.MapPost("/custom-json", async (HttpContext context) => {
    var options = new JsonSerializerOptions
    {
        UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };
    var personFromClient = await context.Request.ReadFromJsonAsync<Person>(options);
    return TypedResults.Json(personFromClient);
});

// Automatic JSON deserialization:
app.MapPost("/automatic-json", (Person personFromClient) => {
    return Results.Ok(personFromClient);
});

// XML deserialization:
app.MapPost("/xml", async (HttpContext context) => {
    var streamReader = new StreamReader(context.Request.Body);
    var body = await streamReader.ReadToEndAsync();
    var xmlSerializer = new XmlSerializer(typeof(Person));
    var stringReader = new StringReader(body);
    var personFromClient = (Person)xmlSerializer.Deserialize(stringReader)!;
    return TypedResults.Ok(personFromClient);
});

// Deserialize data from stored files:
app.MapGet("/load-from-file", () => DeserializeFromFiles());


app.Run();



// Deserialization Techniques for File Storage:
static void DeserializeFromFiles()
{
    // Binary deserialization
    using (var fs = new FileStream("data/person.dat", FileMode.Open))
    using (var reader = new BinaryReader(fs))
    {
        var deserializedPerson = new Person
        {
            FirstName = reader.ReadString(),
            LastName = reader.ReadString(),
            Age = reader.ReadInt32()
        };
        Console.WriteLine($"Binary Deserialization - {deserializedPerson.toString()}");
    }

    // XML Deserialization:
    var serializer = new XmlSerializer(typeof(Person));
    var xmlString = File.ReadAllText("data/person.xml");
    using (var reader = new StringReader(xmlString))
    {
        var deserializedPerson = (Person) serializer.Deserialize(reader);
        Console.WriteLine($"XML Deserialization - {deserializedPerson.toString()}");
    }

    // JSON Serialization:
    try
    {
        string jsonString = File.ReadAllText("data/person.json");
        var deserializedPerson = JsonSerializer.Deserialize<Person>(jsonString);
        if (string.IsNullOrEmpty(deserializedPerson.FirstName))
        {
            throw new Exception("FirstName is required");
        }
        Console.WriteLine("Data Integrity Verified");
        Console.WriteLine($"JSON Deserialization - {deserializedPerson.toString()}");
    }
    catch (JsonException ex)
    {
        Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
    }
}



public class Person
{
    public required string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }

    public string toString()
    {
        return $"FirstName: {FirstName}, LastName: {LastName}, Age: {Age}";
    }
}
