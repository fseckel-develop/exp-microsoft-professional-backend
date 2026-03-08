// Check out .azure/ and .github/ at the root of the repository to find 
// the files 'H42_PipelineGenerationDemo.yml' related to this project 

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Let´s generate a CI/CD Pipeline with Copilot!");

app.Run();
