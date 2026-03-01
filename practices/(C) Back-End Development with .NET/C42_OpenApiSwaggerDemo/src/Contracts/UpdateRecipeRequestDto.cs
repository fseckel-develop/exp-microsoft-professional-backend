namespace OpenApiSwaggerDemo.Contracts;

public sealed class UpdateRecipeRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
}