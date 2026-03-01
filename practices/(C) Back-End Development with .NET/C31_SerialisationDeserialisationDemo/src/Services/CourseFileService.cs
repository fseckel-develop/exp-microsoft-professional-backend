using System.Text.Json;
using System.Xml.Serialization;
using SerializationDeserializationDemo.Models;

namespace SerializationDeserializationDemo.Services;

public sealed class CourseFileService
{
    private const string DataDirectory = "Data";

    public void SaveAllFormats(Course course)
    {
        Directory.CreateDirectory(DataDirectory);

        SaveBinary(course);
        SaveXml(course);
        SaveJson(course);
    }

    public (Course? BinaryCourse, Course? XmlCourse, Course? JsonCourse) LoadAllFormats()
    {
        var binary = LoadBinary();
        var xml = LoadXml();
        var json = LoadJson();

        return (binary, xml, json);
    }

    private static void SaveBinary(Course course)
    {
        using var stream = new FileStream(Path.Combine(DataDirectory, "course.dat"), FileMode.Create);
        using var writer = new BinaryWriter(stream);

        writer.Write(course.Title);
        writer.Write(course.Instructor ?? string.Empty);
        writer.Write(course.DurationHours);
    }

    private static Course? LoadBinary()
    {
        var path = Path.Combine(DataDirectory, "course.dat");

        if (!File.Exists(path))
            return null;

        using var stream = new FileStream(path, FileMode.Open);
        using var reader = new BinaryReader(stream);

        return new Course
        {
            Title = reader.ReadString(),
            Instructor = reader.ReadString(),
            DurationHours = reader.ReadInt32()
        };
    }

    private static void SaveXml(Course course)
    {
        var serializer = new XmlSerializer(typeof(Course));

        using var writer = new StreamWriter(Path.Combine(DataDirectory, "course.xml"));
        serializer.Serialize(writer, course);
    }

    private static Course? LoadXml()
    {
        var path = Path.Combine(DataDirectory, "course.xml");

        if (!File.Exists(path))
            return null;

        var serializer = new XmlSerializer(typeof(Course));
        var xml = File.ReadAllText(path);

        using var reader = new StringReader(xml);
        return (Course?)serializer.Deserialize(reader);
    }

    private static void SaveJson(Course course)
    {
        var json = JsonSerializer.Serialize(course, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(Path.Combine(DataDirectory, "course.json"), json);
    }

    private static Course? LoadJson()
    {
        var path = Path.Combine(DataDirectory, "course.json");

        if (!File.Exists(path))
            return null;

        var json = File.ReadAllText(path);

        try
        {
            return JsonSerializer.Deserialize<Course>(json);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}