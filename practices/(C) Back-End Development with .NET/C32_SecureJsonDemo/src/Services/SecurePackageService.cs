using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using SecureJsonDemo.Models;

namespace SecureJsonDemo.Services;

public sealed class SecurePackageService
{
    // Demo keys only.
    // In real applications, keys should be stored securely and rotated properly.
    private readonly byte[] _encryptionKey = RandomNumberGenerator.GetBytes(32); // AES-256
    private readonly byte[] _integrityKey = RandomNumberGenerator.GetBytes(32);  // HMAC-SHA256

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    public string SerializePackage(DocumentPackage package)
    {
        ValidatePackage(package);

        var packageToSerialize = new DocumentPackage
        {
            DocumentName = package.DocumentName,
            Recipient = package.Recipient,
            Contents = EncryptToBase64(package.Contents, _encryptionKey)
        };

        packageToSerialize.IntegrityTag = ComputeIntegrityTag(packageToSerialize, _integrityKey);

        return JsonSerializer.Serialize(packageToSerialize, _jsonOptions);
    }

    public DocumentPackage? DeserializePackage(string jsonData, bool isTrustedSource)
    {
        if (!isTrustedSource)
            return null;

        DocumentPackage? package;
        try
        {
            package = JsonSerializer.Deserialize<DocumentPackage>(jsonData);
        }
        catch (JsonException)
        {
            return null;
        }

        if (package is null || string.IsNullOrWhiteSpace(package.IntegrityTag))
            return null;

        var expectedTag = ComputeIntegrityTag(package, _integrityKey);

        if (!CryptographicEquals(package.IntegrityTag, expectedTag))
            return null;

        try
        {
            package.Contents = DecryptFromBase64(package.Contents, _encryptionKey);
        }
        catch (CryptographicException)
        {
            return null;
        }
        catch (FormatException)
        {
            return null;
        }

        return package;
    }

    private static void ValidatePackage(DocumentPackage package)
    {
        if (package is null)
            throw new ArgumentNullException(nameof(package));

        if (string.IsNullOrWhiteSpace(package.DocumentName))
            throw new ArgumentException("Document name is required.", nameof(package));

        if (string.IsNullOrWhiteSpace(package.Recipient))
            throw new ArgumentException("Recipient is required.", nameof(package));

        if (string.IsNullOrWhiteSpace(package.Contents))
            throw new ArgumentException("Contents are required.", nameof(package));
    }

    private static string ComputeIntegrityTag(DocumentPackage package, byte[] hmacKey)
    {
        var unsignedPayload = new
        {
            package.DocumentName,
            package.Recipient,
            package.Contents
        };

        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(unsignedPayload));

        using var hmac = new HMACSHA256(hmacKey);
        return Convert.ToBase64String(hmac.ComputeHash(bytes));
    }

    private static string EncryptToBase64(string plaintext, byte[] key)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var plainBytes = Encoding.UTF8.GetBytes(plaintext);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        var combined = new byte[aes.IV.Length + cipherBytes.Length];
        Buffer.BlockCopy(aes.IV, 0, combined, 0, aes.IV.Length);
        Buffer.BlockCopy(cipherBytes, 0, combined, aes.IV.Length, cipherBytes.Length);

        return Convert.ToBase64String(combined);
    }

    private static string DecryptFromBase64(string base64, byte[] key)
    {
        var combined = Convert.FromBase64String(base64);

        using var aes = Aes.Create();
        aes.Key = key;

        var ivLength = aes.BlockSize / 8;
        if (combined.Length <= ivLength)
            throw new CryptographicException("Invalid encrypted payload.");

        var iv = new byte[ivLength];
        var cipher = new byte[combined.Length - ivLength];

        Buffer.BlockCopy(combined, 0, iv, 0, ivLength);
        Buffer.BlockCopy(combined, ivLength, cipher, 0, cipher.Length);

        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var plainBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }

    private static bool CryptographicEquals(string aBase64, string bBase64)
    {
        var a = Convert.FromBase64String(aBase64);
        var b = Convert.FromBase64String(bBase64);

        if (a.Length != b.Length)
            return false;

        return CryptographicOperations.FixedTimeEquals(a, b);
    }
}