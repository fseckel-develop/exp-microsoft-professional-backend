using System.Security.Cryptography;
using FileEncryptionDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileEncryptionDemo.Pages;

public class IndexModel : PageModel
{
    private readonly IFileCryptoService _crypto;
    private readonly IWebHostEnvironment _env;

    public IndexModel(IFileCryptoService crypto, IWebHostEnvironment env)
    {
        _crypto = crypto;
        _env = env;
    }

    [BindProperty]
    public IFormFile? UploadedFile { get; set; }

    public string? ResultMessage { get; private set; }
    public string? OutputFileName { get; private set; }
    public string? ErrorMessage { get; private set; }

    public async Task<IActionResult> OnPostEncryptAsync(CancellationToken ct)
        => await HandleAsync(mode: "encrypt", ct);

    public async Task<IActionResult> OnPostDecryptAsync(CancellationToken ct)
        => await HandleAsync(mode: "decrypt", ct);

    private async Task<IActionResult> HandleAsync(string mode, CancellationToken ct)
    {
        if (UploadedFile is null || UploadedFile.Length == 0)
        {
            ErrorMessage = "Please upload a file first.";
            return Page();
        }

        var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
        var processedDir = Path.Combine(_env.WebRootPath, "processed");
        Directory.CreateDirectory(uploadsDir);
        Directory.CreateDirectory(processedDir);

        var safeName = Path.GetFileName(UploadedFile.FileName); // prevents ../ tricks
        var inputPath = Path.Combine(uploadsDir, safeName);

        await using (var stream = System.IO.File.Create(inputPath))
        {
            await UploadedFile.CopyToAsync(stream, ct);
        }

        try
        {
            if (mode == "encrypt")
            {
                var outName = $"encrypted_{safeName}";
                var outputPath = Path.Combine(processedDir, outName);

                await _crypto.EncryptFileAsync(inputPath, outputPath, ct);

                OutputFileName = outName;
                ResultMessage = "File encrypted successfully.";
            }
            else
            {
                var outName = $"decrypted_{safeName}";
                var outputPath = Path.Combine(processedDir, outName);

                await _crypto.DecryptFileAsync(inputPath, outputPath, ct);

                OutputFileName = outName;
                ResultMessage = "File decrypted successfully.";
            }
        }
        catch (CryptographicException)
        {
            ErrorMessage = "Crypto operation failed. Wrong key or invalid encrypted file.";
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        return Page();
    }
}