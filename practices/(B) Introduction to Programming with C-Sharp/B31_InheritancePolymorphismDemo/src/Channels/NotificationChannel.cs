using InheritancePolymorphismDemo.Models;

namespace InheritancePolymorphismDemo.Channels;

public abstract class NotificationChannel
{
    public abstract string Name { get; }

    public void Process(NotificationMessage message)
    {
        Validate(message);
        Console.WriteLine($"--- {Name} ---");
        Send(message);
        Console.WriteLine();
    }

    protected virtual void Validate(NotificationMessage message)
    {
        if (string.IsNullOrWhiteSpace(message.Recipient))
            throw new ArgumentException("Recipient is required.", nameof(message));

        if (string.IsNullOrWhiteSpace(message.Body))
            throw new ArgumentException("Message body is required.", nameof(message));
    }

    protected abstract void Send(NotificationMessage message);
}