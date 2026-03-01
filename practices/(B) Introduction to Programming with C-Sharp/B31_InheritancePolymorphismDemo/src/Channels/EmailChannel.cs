using InheritancePolymorphismDemo.Models;

namespace InheritancePolymorphismDemo.Channels;

public sealed class EmailChannel : NotificationChannel
{
    public override string Name => "Email";

    protected override void Send(NotificationMessage message)
    {
        Console.WriteLine($"To: {message.Recipient}");
        Console.WriteLine($"Subject: {message.Subject}");
        Console.WriteLine($"Body: {message.Body}");
        Console.WriteLine("Email sent via SMTP gateway.");
    }
}