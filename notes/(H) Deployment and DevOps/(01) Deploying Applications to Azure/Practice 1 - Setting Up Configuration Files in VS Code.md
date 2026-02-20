## Overview

- Focus: Secure configuration management in local development  
	- Protect sensitive data (API keys, connection strings)  
	- Separate configuration from source code  
- Tools used:  
	- Visual Studio Code  
	- ASP.NET Core  
	- Open-source library support  
- Reinforces best practices for future deployment to Azure App Services  

---
## Setup Implementation Steps

### Step 1: Set Up ASP.NET Core Web API Project

- Create a new project:  `dotnet new webapi -n MyApp`  

- Navigate into project directory:  `cd MyApp`  

- Open project in VS Code:  `code .`  

- Establishes the base application for applying configuration practices  

### Step 2: Add Environment Variable Support

- Install DotNetEnv package:  `dotnet add package DotNetEnv`  

- Create a new file named `.env`  
- Enables loading configuration values from an external file  
- Supports separation of configuration from application logic  

### Step 3: Create the .env File

- Add key-value pairs inside `.env`:  

```env
ASPNETCORE_ENVIRONMENT=Development
API_KEY=SuperSecret123
```

- Stores sensitive settings outside the source code  
- Mirrors cloud-based configuration practices  

### Step 4: Load Environment Variables in Program.cs

- Add before `builder.Build()`:

```C#
DotNetEnv.Env.Load(); 
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var apiKey = Environment.GetEnvironmentVariable("API_KEY");
```

- Loads variables into runtime memory  
- Ensures secure access to configuration values  
- Prevents hard-coded secrets in repositories  

### Step 5: Test the Setup Locally

- Add test output after defining variables:

```C#
Console.WriteLine($"Running in: {environment}");
Console.WriteLine($"Using API Key: {apiKey}");
```

- Run the application with:  `dotnet run`  

- Confirms environment variables are properly loaded  
- Validates secure configuration handling during development  

---
## Key Learning Outcomes

- Understand secure handling of sensitive data  
- Implement environment-based configuration  
- Apply separation of concerns between code and configuration  
- Build a strong foundation for Azure cloud deployment  