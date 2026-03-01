namespace OpenApiSwaggerDemo.Models;

public sealed class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
}