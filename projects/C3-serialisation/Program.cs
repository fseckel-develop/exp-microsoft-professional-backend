using System.Text.Json;
using System.Xml.Serialization;


var person = new Person {
    FirstName = "John",
    LastName = "Doe",
    Age = 30
};


var builder = WebApplication.CreateBuilder(args);



// Global policies for JSON serialization:
// will ALSO force KebabCaseUpper for Deserialization (POST endpoints)
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseUpper;
    options.SerializerOptions.WriteIndented = true;
});



var app = builder.Build();



// SERIALIZATION EXAMPLES:

// Manual JSON serialization:
app.MapGet("/manual-json", () => {
    var jsonOutput = JsonSerializer.Serialize(person);
    return TypedResults.Text(jsonOutput, "application/json");
});

// Custom JSON serialization with options:
app.MapGet("/custom-json", () => {
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };
    var jsonOutput = JsonSerializer.Serialize(person, options);
    return TypedResults.Text(jsonOutput, "application/json");
});

// Automatic JSON serialization (using Results.Json):
app.MapGet("/automatic-json", () => { return TypedResults.Json(person); });

// Automatic JSON serialization (just returning the object):
app.MapGet("/simple-json", () => { return person; });

// Manual XML serialization:
app.MapGet("/manual-xml", () => {
    var xmlSerializer = new XmlSerializer(typeof(Person));
    using var stringWriter = new StringWriter();
    xmlSerializer.Serialize(stringWriter, person);
    var xmlOutput = stringWriter.ToString();
    return TypedResults.Text(xmlOutput, "application/xml");
});

// Store serialized data to files:
app.MapGet("/store-to-file", () => SerializeToFiles());



app.Run();



// Serialization Techniques for File Storage:
static void SerializeToFiles()
{
    var samplePerson = new Person
    {
        FirstName = "Alice",
        LastName = "Smith",
        Age = 28
    };

    // Binary Serialization:
    using (FileStream fs = new FileStream("person.dat", FileMode.Create))
    {
        BinaryWriter writer = new BinaryWriter(fs);
        writer.Write(samplePerson.FirstName);
        writer.Write(samplePerson.LastName);
        writer.Write(samplePerson.Age);
    }
    Console.WriteLine("Binary serialization complete.");

    // XML Serialization:
    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
    using (StreamWriter writer = new StreamWriter("person.xml"))
    {
        xmlSerializer.Serialize(writer, samplePerson);
    }
    Console.WriteLine("XML serialization complete.");

    // JSON Serialization:
    string jsonString = JsonSerializer.Serialize(samplePerson);
    File.WriteAllText("person.json", jsonString);
    Console.WriteLine("JSON serialization complete.");
}


public class Person
{
    public required string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
}
