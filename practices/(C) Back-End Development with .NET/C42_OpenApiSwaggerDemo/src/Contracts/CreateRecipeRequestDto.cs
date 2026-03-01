namespace OpenApiSwaggerDemo.Contracts;

public sealed class CreateRecipeRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
}