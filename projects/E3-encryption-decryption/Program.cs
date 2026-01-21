using System.Text;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEncryptionService, EncryptionService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/encrypt", (EncryptionRequest request, IEncryptionService encryptionService) =>
{
    var encryptedText = encryptionService.Encrypt(request.PlainText);
    return Results.Ok(new { EncryptedText = encryptedText });
});

app.MapPost("/decrypt", (DecryptionRequest request, IEncryptionService encryptionService) =>
{
    var decryptedText = encryptionService.Decrypt(request.CypherText);
    return Results.Ok(new { DecryptedText = decryptedText });
});

app.Run();


public record EncryptionRequest(string PlainText);
public record DecryptionRequest(string CypherText);

public interface IEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cypherText);
}

internal class EncryptionService : IEncryptionService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;  // initialization vector (secondary key for encryption algorithms)

    public EncryptionService(IConfiguration configuration)
    {
        _key = Encoding.UTF8.GetBytes(configuration["Encryption:Key"]!);
        _iv = Encoding.UTF8.GetBytes(configuration["Encryption:IV"]!);
    }

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(plainText);
            streamWriter.Flush();
        }
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string cypherText)
    {
        if (string.IsNullOrEmpty(cypherText))
            throw new ArgumentNullException(nameof(cypherText));
        var buffer = Convert.FromBase64String(cypherText);
        
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }
}
