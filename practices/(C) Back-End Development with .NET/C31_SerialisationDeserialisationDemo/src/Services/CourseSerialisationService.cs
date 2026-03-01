using System.Text.Json;
using System.Xml.Serialization;
using SerializationDeserializationDemo.Models;

namespace SerializationDeserializationDemo.Services;

public sealed class CourseSerializationService
{
    public string SerializeToJson(Course course, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(course, options);
    }

    public Course? DeserializeFromJson(string json, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<Course>(json, options);
    }

    public string SerializeToXml(Course course)
    {
        var serializer = new XmlSerializer(typeof(Course));

        using var writer = new StringWriter();
        serializer.Serialize(writer, course);

        return writer.ToString();
    }

    public Course DeserializeFromXml(string xml)
    {
        var serializer = new XmlSerializer(typeof(Course));

        using var reader = new StringReader(xml);
        return (Course)serializer.Deserialize(reader)!;
    }
}