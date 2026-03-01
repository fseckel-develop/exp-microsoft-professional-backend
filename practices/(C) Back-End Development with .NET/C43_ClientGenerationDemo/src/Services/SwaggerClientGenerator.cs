using NSwag;
using NSwag.CodeGeneration.CSharp;
using ClientGenerationDemo.Configuration;

namespace ClientGenerationDemo.Services;

public sealed class SwaggerClientGenerator
{
    private readonly ApiClientOptions _options;

    public SwaggerClientGenerator(ApiClientOptions options)
    {
        _options = options;
    }

    public async Task GenerateClientAsync(string outputDirectory = "Generated")
    {
        Directory.CreateDirectory(outputDirectory);

        using var httpClient = new HttpClient();

        var swaggerJson = await httpClient.GetStringAsync(
            _options.BaseUrl.TrimEnd('/') + _options.SwaggerPath);

        var document = await OpenApiDocument.FromJsonAsync(swaggerJson);

        var settings = new CSharpClientGeneratorSettings
        {
            ClassName = _options.GeneratedClientName,
            CSharpGeneratorSettings =
            {
                Namespace = _options.GeneratedClientNamespace
            }
        };

        var generator = new CSharpClientGenerator(document, settings);
        var code = generator.GenerateFile();

        var outputPath = Path.Combine(outputDirectory, $"{_options.GeneratedClientName}.cs");
        await File.WriteAllTextAsync(outputPath, code);

        Console.WriteLine($"Generated client: {outputPath}");
    }
}