namespace SecureDataStorageDemo.Infrastructure;

public sealed class EncryptionOptions
{
    public const string SectionName = "Encryption";

    // Base64 key (16/24/32 bytes decoded)
    public string KeyBase64 { get; set; } = string.Empty;
}