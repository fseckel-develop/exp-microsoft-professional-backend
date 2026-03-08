# (H22) AzureDevOpsDemo

A minimal ASP.NET Core application demonstrating how Azure DevOps pipelines can automatically build and test a .NET project using YAML-based CI/CD workflows triggered by repository events.

---
## Course Context

**Course**: Deployment and DevOps 
**Section**: CI/CD Pipelines with Azure DevOps

This project demonstrates how Azure DevOps can automate the build and testing process for a .NET application. It illustrates how YAML pipelines connect to a source repository and execute automated restore, build, and test steps whenever code changes occur.

---
## Concepts Demonstrated

- Continuous Integration (CI) with Azure DevOps
- YAML-based pipeline configuration
- Azure DevOps pipeline triggers
- Automated dependency restoration
- Automated build and test execution
- Integration between GitHub and Azure DevOps
- CI pipeline monitoring and troubleshooting

---
## Project Structure

- **RepoRoot/.azure/workflows** – Azure DevOps pipeline configuration for CI pipeline automation
- **src/Program.cs** – Minimal ASP.NET Core application used to trigger pipeline runs