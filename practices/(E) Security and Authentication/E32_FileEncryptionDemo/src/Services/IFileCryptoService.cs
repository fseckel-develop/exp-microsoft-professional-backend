namespace FileEncryptionDemo.Services;

public interface IFileCryptoService
{
    Task EncryptFileAsync(string inputPath, string outputPath, CancellationToken ct = default);
    Task DecryptFileAsync(string inputPath, string outputPath, CancellationToken ct = default);
}