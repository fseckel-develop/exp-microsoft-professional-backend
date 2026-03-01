using InheritancePolymorphismDemo.Channels;
using InheritancePolymorphismDemo.Models;

namespace InheritancePolymorphismDemo;

internal static class Program
{
    private static void Main()
    {
        var message = new NotificationMessage(
            Recipient: "customer-123",
            Subject: "Order Update",
            Body: "Your order has been shipped and is on its way.");

        List<NotificationChannel> channels =
        [
            new EmailChannel(),
            new SmsChannel(),
            new PushChannel()
        ];

        foreach (var channel in channels)
        {
            channel.Process(message);
        }
    }
}