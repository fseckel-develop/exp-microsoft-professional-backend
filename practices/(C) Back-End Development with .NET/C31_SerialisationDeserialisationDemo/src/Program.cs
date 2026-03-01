using System.Text.Json;
using SerializationDeserializationDemo.Models;
using SerializationDeserializationDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Global JSON configuration used by automatic JSON endpoints.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
});

builder.Services.AddSingleton<CourseSerializationService>();
builder.Services.AddSingleton<CourseFileService>();

var app = builder.Build();

var sampleCourse = new Course
{
    Title = "Introduction to Cloud Computing",
    Instructor = "Dr. Miller",
    DurationHours = 24
};

//
// Serialization endpoints
//

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        message = "Serialization and deserialization demo is running."
    });
});

app.MapGet("/serialize/manual-json", (CourseSerializationService serializer) =>
{
    var json = serializer.SerializeToJson(sampleCourse);
    return TypedResults.Text(json, "application/json");
});

app.MapGet("/serialize/custom-json", (CourseSerializationService serializer) =>
{
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };

    var json = serializer.SerializeToJson(sampleCourse, options);
    return TypedResults.Text(json, "application/json");
});

app.MapGet("/serialize/automatic-json", () =>
{
    return TypedResults.Json(sampleCourse);
});

app.MapGet("/serialize/simple-json", () =>
{
    return sampleCourse;
});

app.MapGet("/serialize/manual-xml", (CourseSerializationService serializer) =>
{
    var xml = serializer.SerializeToXml(sampleCourse);
    return TypedResults.Text(xml, "application/xml");
});

//
// Deserialization endpoints
//

app.MapPost("/deserialize/manual-json", async (HttpContext context, CourseSerializationService serializer) =>
{
    var course = await context.Request.ReadFromJsonAsync<Course>();

    if (course is null)
        return Results.BadRequest("Invalid JSON payload.");

    return Results.Ok(course);
});

app.MapPost("/deserialize/custom-json", async (HttpContext context, CourseSerializationService serializer) =>
{
    var options = new JsonSerializerOptions
    {
        UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };

    var course = await context.Request.ReadFromJsonAsync<Course>(options);

    if (course is null)
        return Results.BadRequest("Invalid JSON payload.");

    return Results.Ok(course);
});

app.MapPost("/deserialize/automatic-json", (Course course) =>
{
    return Results.Ok(course);
});

app.MapPost("/deserialize/xml", async (HttpContext context, CourseSerializationService serializer) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    var course = serializer.DeserializeFromXml(body);
    return Results.Ok(course);
});

//
// File persistence endpoints
//

app.MapGet("/files/store", (CourseFileService fileService) =>
{
    var course = new Course
    {
        Title = "Advanced API Development",
        Instructor = "Prof. Johnson",
        DurationHours = 32
    };

    fileService.SaveAllFormats(course);

    return Results.Ok(new
    {
        message = "Course data stored in binary, XML, and JSON files."
    });
});

app.MapGet("/files/load", (CourseFileService fileService) =>
{
    var result = fileService.LoadAllFormats();

    return Results.Ok(new
    {
        BinaryFound = result.BinaryCourse != null,
        XmlFound = result.XmlCourse != null,
        JsonFound = result.JsonCourse != null,

        Binary = result.BinaryCourse,
        Xml = result.XmlCourse,
        Json = result.JsonCourse
    });
});

app.Run();