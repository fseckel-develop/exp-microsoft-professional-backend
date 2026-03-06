using System.Security.Claims;
using JwtCreationDecodingDemo.Models;

namespace JwtCreationDecodingDemo.Presentation;

public static class ConsoleWriter
{
    public static void WriteToken(string token)
    {
        Console.WriteLine("Generated token:\n");
        Console.WriteLine(token);
        Console.WriteLine();
    }

    public static void WriteValidationResult(JwtValidationResult result)
    {
        if (!result.IsValid)
        {
            Console.WriteLine($"Token validation failed: {result.Error}\n");
            return;
        }

        Console.WriteLine("Token validated successfully.\n");

        if (result.Principal != null)
        {
            Console.WriteLine("Decoded claims:");
            WriteClaims(result.Principal.Claims);
        }
    }

    public static void WriteClaims(IEnumerable<Claim> claims)
    {
        Console.WriteLine("Decoded claims:");
        foreach (var claim in claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }
        Console.WriteLine();
    }
}