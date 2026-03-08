// Check out .azure/ and .github/ at the root of the repository to find 
// the files 'H23_AlertOnFailureDemo.yml' related to this project 

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Let's trigger an CI/CD Pipeline Alert!");

// Uncomment the following line to simulate a failure
TriggerFailure();

app.Run();
