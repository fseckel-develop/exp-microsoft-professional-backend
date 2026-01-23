using NSwag;
using NSwag.CodeGeneration.CSharp;


public class SwaggerClientGenerator
{
    private readonly string apiBaseURL = "http://localhost:5221";
    private readonly string clientName = "BlogApiClient";

    public async Task GenerateClient()
    {
        var httpClient = new HttpClient();
        var swaggerJson = await httpClient.GetStringAsync(apiBaseURL + "/swagger/v1/swagger.json");
        var document = await OpenApiDocument.FromJsonAsync(swaggerJson);
        var settings = new CSharpClientGeneratorSettings
        {
            ClassName = clientName,
            CSharpGeneratorSettings =
            {
                Namespace = "BlogApi"
            }
        };

        var generator = new CSharpClientGenerator(document, settings);

        var code = generator.GenerateFile();

        await File.WriteAllTextAsync(clientName + ".cs", code);
    }
}
