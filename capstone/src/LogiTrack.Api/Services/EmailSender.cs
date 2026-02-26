using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;

public class FakeEmailSender : IEmailSender
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

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(
            _configuration["Email:FromName"], 
            _configuration["Email:FromEmail"] ?? ""
        ));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        using var client = new SmtpClient();
        await client.ConnectAsync(
            _configuration["Email:SmtpServer"], 
            int.Parse(_configuration["Email:Port"] ?? "587"), 
            true
        );
        await client.AuthenticateAsync(
            _configuration["Email:Username"], 
            _configuration["Email:Password"]
        );
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}