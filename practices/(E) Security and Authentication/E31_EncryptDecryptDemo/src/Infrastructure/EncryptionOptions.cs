namespace EncryptDecryptDemo.Infrastructure;

public sealed class EncryptionOptions
{
    public const string SectionName = "Encryption";

    // Recommended: store as Base64 in config (not raw text)
    public string KeyBase64 { get; set; } = string.Empty;
}