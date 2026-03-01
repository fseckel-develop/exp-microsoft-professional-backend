namespace OpenApiSwaggerDemo.Contracts;

public sealed class CreateCookbookRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int RecipeCount { get; set; }
}