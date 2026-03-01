using ApiExceptionHandlingDemo.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ApiExceptionHandlingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TextAnalysisController : ControllerBase
{
    [HttpGet("average-word-length")]
    public IActionResult GetAverageWordLength([FromQuery] string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ValidationException("Input text cannot be empty.");

        var words = text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Where(w => w.Any(char.IsLetter))
            .ToArray();

        if (words.Length == 0)
            throw new ProcessingException("No valid words found.");

        var averageLength = words.Average(w => w.Length);

        return Ok(new
        {
            AverageWordLength = averageLength
        });
    }
}