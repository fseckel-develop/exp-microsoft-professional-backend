using JwtCreationDecodingDemo.Models;
using JwtCreationDecodingDemo.Services;
using JwtCreationDecodingDemo.Presentation;

internal static class Program
{
    private static void Main()
    {
        var options = JwtOptions.ForDemo();
        var jwt = new JwtService(options);

        var token = jwt.IssueToken(new JwtSubject(
            UserId: "user123",
            Roles: new[] { "admin" }
        ));

        ConsoleWriter.WriteToken(token);

        var result = jwt.Validate(token);

        ConsoleWriter.WriteValidationResult(result);

        if (result.IsValid && result.Principal is not null)
        {
            ConsoleWriter.WriteClaims(result.Principal.Claims);
        }
    }
}