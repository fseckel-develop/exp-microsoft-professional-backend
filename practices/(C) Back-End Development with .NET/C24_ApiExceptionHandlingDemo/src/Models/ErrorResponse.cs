namespace ApiExceptionHandlingDemo.Models;

public sealed class ErrorResponse
{
    public string Error { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public int StatusCode { get; init; }
}