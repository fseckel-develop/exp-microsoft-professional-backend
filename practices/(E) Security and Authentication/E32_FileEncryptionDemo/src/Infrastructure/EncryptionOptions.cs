namespace FileEncryptionDemo.Infrastructure;

public sealed class EncryptionOptions
{
    public const string SectionName = "Encryption";

    // Base64 key, 16/24/32 bytes when decoded
    public string KeyBase64 { get; set; } = string.Empty;
}