namespace SecureDataStorageDemo.Models;

public class Message
{
    public int Id { get; set; }
    public required string CipherText { get; set; }  // encrypted payload (base64)
    public required string UserId { get; set; }      // from JWT subject/nameid
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}