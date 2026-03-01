namespace SecureJsonDemo.Models;

public sealed class DocumentPackage
{
    public required string DocumentName { get; set; }
    public required string Recipient { get; set; }

    // Sensitive content
    public required string Contents { get; set; }

    // Integrity tag stored alongside the payload
    public string? IntegrityTag { get; set; }
}