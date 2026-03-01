namespace ControllerDrivenApi.Contracts;

public sealed class UpdateProductRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}