# (H12) AzureDeploymentDemo

A minimal ASP.NET Core application demonstrating how environment variables and external configuration can be used to prepare applications for secure and consistent deployment to Azure.

---
## Course Context

**Course**: Deployment and DevOps
**Section**: Azure Deployment Preparation

This project illustrates how applications can load environment variables at runtime to avoid hard-coding sensitive configuration. It demonstrates how development environments can simulate Azure App Service settings using a `.env` file.  
  
In production environments:  
- Secrets should never be stored in source code
- Configuration should be injected at runtime
Cloud platforms typically provide configuration through:  
- Azure App Service Application Settings
- Container environment variables
- Secret managers (Azure Key Vault)
- CI/CD pipeline configuration
  
ASP.NET Core applications support these deployment scenarios through a unified configuration system, allowing applications to load settings from multiple providers without requiring changes to application code.

---
## Concepts Demonstrated

- Environment variable configuration
- Secure externalised application settings
- Using .env files during development
- Loading environment variables with DotNetEnv
- ASP.NET Core configuration pipeline
- Configuration binding using the Options pattern
- Environment-specific configuration (appsettings.Development.json)
- Environment-specific application behaviour
- Preparing applications for Azure App Service deployment

---
## Project Structure

- **Program.cs** – Application startup, configuration pipeline setup, and environment variable loading  
- **Configuration/DemoSettings.cs** – Configuration binding class used for runtime settings  
- **appsettings.json** – Default application configuration  
- **appsettings.Development.json** – Development-specific configuration overrides  
- **.env** – Example environment variable configuration used during development