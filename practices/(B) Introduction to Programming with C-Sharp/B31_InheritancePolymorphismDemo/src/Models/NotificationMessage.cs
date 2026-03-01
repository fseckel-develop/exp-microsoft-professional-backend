namespace InheritancePolymorphismDemo.Models;

public sealed record NotificationMessage(
    string Recipient,
    string Subject,
    string Body
);