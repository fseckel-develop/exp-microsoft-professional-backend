// Check out .azure/ at the root of the repository to find 
// the file 'H22_AzureDevOpsDemo.yml' related to this project 

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Let's start with Azure DevOps!");

app.Run();
