using InheritancePolymorphismDemo.Models;

namespace InheritancePolymorphismDemo.Channels;

public sealed class PushChannel : NotificationChannel
{
    public override string Name => "Push Notification";

    protected override void Send(NotificationMessage message)
    {
        Console.WriteLine($"User Device: {message.Recipient}");
        Console.WriteLine($"Title: {message.Subject}");
        Console.WriteLine($"Payload: {message.Body}");
        Console.WriteLine("Push notification sent to mobile app.");
    }
}