namespace SecureDataStorageDemo.Contracts;

public sealed class MessageListItemDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}