using System.Text.Json;
using ApiExceptionHandlingDemo.Exceptions;
using ApiExceptionHandlingDemo.Models;

namespace ApiExceptionHandlingDemo.Middleware;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation error occurred.");

            await WriteErrorResponseAsync(
                context,
                StatusCodes.Status400BadRequest,
                "ValidationError",
                ex.Message);
        }
        catch (ProcessingException ex)
        {
            _logger.LogWarning(ex, "Processing error occurred.");

            await WriteErrorResponseAsync(
                context,
                StatusCodes.Status422UnprocessableEntity,
                "ProcessingError",
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled system error occurred.");

            await WriteErrorResponseAsync(
                context,
                StatusCodes.Status500InternalServerError,
                "SystemError",
                "A system error occurred while processing your request.");
        }
    }

    private static async Task WriteErrorResponseAsync(
        HttpContext context,
        int statusCode,
        string error,
        string message)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var payload = new ErrorResponse
        {
            Error = error,
            Message = message,
            StatusCode = statusCode
        };

        var json = JsonSerializer.Serialize(payload);
        await context.Response.WriteAsync(json);
    }
}