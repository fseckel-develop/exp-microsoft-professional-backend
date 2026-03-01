using InheritancePolymorphismDemo.Models;

namespace InheritancePolymorphismDemo.Channels;

public sealed class SmsChannel : NotificationChannel
{
    public override string Name => "SMS";

    protected override void Validate(NotificationMessage message)
    {
        base.Validate(message);

        if (message.Body.Length > 160)
            throw new ArgumentException("SMS body must not exceed 160 characters.", nameof(message));
    }

    protected override void Send(NotificationMessage message)
    {
        Console.WriteLine($"Phone: {message.Recipient}");
        Console.WriteLine($"Text: {message.Body}");
        Console.WriteLine("SMS sent via telecom provider.");
    }
}