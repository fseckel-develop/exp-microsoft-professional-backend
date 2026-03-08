// Check out .github/ at the root of the repository to find 
// the file 'H21_GitHubActionsDemo.yml' related to this project 

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Let´s start with GitHub Actions!");

app.Run();
