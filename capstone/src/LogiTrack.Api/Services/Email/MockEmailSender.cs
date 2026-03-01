using Microsoft.AspNetCore.Identity.UI.Services;

namespace LogiTrack.Api.Services.Email;

public class MockEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine("=== EMAIL SENT ===");
        Console.WriteLine($"To: {email}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {htmlMessage}");
        Console.WriteLine("==================");
        return Task.CompletedTask;
    }
}