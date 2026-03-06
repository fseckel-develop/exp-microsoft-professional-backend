using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using FileEncryptionDemo.Infrastructure;

namespace FileEncryptionDemo.Services;

public sealed class AesFileCryptoService : IFileCryptoService
{
    private readonly byte[] _key;

    public AesFileCryptoService(IOptions<EncryptionOptions> options)
    {
        _key = ReadKey(options.Value.KeyBase64);
    }

    public async Task EncryptFileAsync(string inputPath, string outputPath, CancellationToken ct = default)
    {
        await using var input = File.OpenRead(inputPath);
        await using var output = File.Create(outputPath);

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        aes.GenerateIV();
        var iv = aes.IV;

        // write IV first
        await output.WriteAsync(iv, ct);

        using var encryptor = aes.CreateEncryptor(aes.Key, iv);
        await using var crypto = new CryptoStream(output, encryptor, CryptoStreamMode.Write, leaveOpen: true);

        await input.CopyToAsync(crypto, ct);
        await crypto.FlushAsync(ct);
    }

    public async Task DecryptFileAsync(string inputPath, string outputPath, CancellationToken ct = default)
    {
        await using var input = File.OpenRead(inputPath);
        await using var output = File.Create(outputPath);

        var iv = new byte[16];
        var read = await input.ReadAsync(iv, 0, iv.Length, ct);
        if (read != 16)
            throw new InvalidOperationException("Encrypted file is missing IV header.");

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        await using var crypto = new CryptoStream(input, decryptor, CryptoStreamMode.Read);

        await crypto.CopyToAsync(output, ct);
    }

    private static byte[] ReadKey(string keyBase64)
    {
        byte[] key;
        try
        {
            key = Convert.FromBase64String(keyBase64);
        }
        catch (FormatException)
        {
            throw new InvalidOperationException("Encryption:KeyBase64 must be valid Base64.");
        }

        if (key.Length is not (16 or 24 or 32))
            throw new InvalidOperationException("Encryption key must be 16, 24, or 32 bytes (Base64-decoded).");

        return key;
    }
}