using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using SecureDataStorageDemo.Infrastructure;

namespace SecureDataStorageDemo.Services;

public sealed class AesMessageCryptoService : IMessageCryptoService
{
    private readonly byte[] _key;

    public AesMessageCryptoService(IOptions<EncryptionOptions> options)
    {
        _key = ReadKey(options.Value.KeyBase64);
    }

    public string EncryptToBase64(string plaintext)
    {
        if (plaintext is null) throw new ArgumentNullException(nameof(plaintext));

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        aes.GenerateIV();
        var iv = aes.IV;

        using var enc = aes.CreateEncryptor(aes.Key, iv);

        var plainBytes = Encoding.UTF8.GetBytes(plaintext);
        var cipherBytes = enc.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        var payload = new byte[iv.Length + cipherBytes.Length];
        Buffer.BlockCopy(iv, 0, payload, 0, iv.Length);
        Buffer.BlockCopy(cipherBytes, 0, payload, iv.Length, cipherBytes.Length);

        return Convert.ToBase64String(payload);
    }

    public string DecryptFromBase64(string payloadBase64)
    {
        if (string.IsNullOrWhiteSpace(payloadBase64))
            throw new ArgumentException("Cipher payload is required.", nameof(payloadBase64));

        byte[] payload;
        try { payload = Convert.FromBase64String(payloadBase64); }
        catch (FormatException) { throw new ArgumentException("Cipher payload is not valid Base64.", nameof(payloadBase64)); }

        if (payload.Length < 16 + 1)
            throw new ArgumentException("Cipher payload is too short.", nameof(payloadBase64));

        var iv = payload[..16];
        var cipher = payload[16..];

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var dec = aes.CreateDecryptor(aes.Key, aes.IV);
        var plainBytes = dec.TransformFinalBlock(cipher, 0, cipher.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }

    private static byte[] ReadKey(string keyBase64)
    {
        byte[] key;
        try { key = Convert.FromBase64String(keyBase64); }
        catch (FormatException) { throw new InvalidOperationException("Encryption:KeyBase64 must be valid Base64."); }

        if (key.Length is not (16 or 24 or 32))
            throw new InvalidOperationException("Encryption key must decode to 16, 24, or 32 bytes.");

        return key;
    }
}