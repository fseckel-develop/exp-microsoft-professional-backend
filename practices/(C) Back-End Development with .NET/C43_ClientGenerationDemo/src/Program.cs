using ClientGenerationDemo.Configuration;
using ClientGenerationDemo.Demo;
using ClientGenerationDemo.Presentation;
using ClientGenerationDemo.Services;
#if GENERATED_CLIENT
using GeneratedRecipeApi;
#endif

// NOTE:
// The compile-time symbol GENERATED_CLIENT is set automatically by the 
// file 'ClientGenerationDemo.csproj' once the client has been generated.
// So in the next program run the generated client is used as well.

namespace ClientGenerationDemo;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var options = new ApiClientOptions();
        var writer = new ConsoleWriter();

        writer.WriteTitle("Client Generation Demo");

        await RunClientGenerationAsync(options, writer);
        await RunManualClientAsync(options, writer);

#if GENERATED_CLIENT
        await RunGeneratedClientAsync(options, writer);
#else
        writer.WriteMessage(
            "Generated client demo is skipped because the client has not been generated yet.");
#endif
    }

    private static async Task RunClientGenerationAsync(ApiClientOptions options, ConsoleWriter writer)
    {
        writer.WriteSection("NSwag Client Generation");

        var generator = new SwaggerClientGenerator(options);
        await generator.GenerateClientAsync();

        writer.WriteMessage("Client generation completed.");
    }

    private static async Task RunManualClientAsync(ApiClientOptions options, ConsoleWriter writer)
    {
        using var httpClient = new HttpClient();

        var manualClient = new ManualApiClient(httpClient, options.BaseUrl);
        var demo = new ManualClientDemo(manualClient, writer);

        await demo.RunAsync();
    }

#if GENERATED_CLIENT
    private static async Task RunGeneratedClientAsync(ApiClientOptions options, ConsoleWriter writer)
    {
        using var httpClient = new HttpClient();

        var generatedClient = new GeneratedApiClient(options.BaseUrl, httpClient);
        var demo = new GeneratedClientDemo(generatedClient, writer);

        await demo.RunAsync();
    }
#endif
}