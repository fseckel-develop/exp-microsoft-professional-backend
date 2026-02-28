# (H42) PipelineGenerationDemo

A minimal ASP.NET Core application demonstrating how AI tools such as Microsoft Copilot can assist in generating CI/CD pipeline configurations. The project showcases how AI can help create YAML pipelines for automated build, test, and deployment workflows using platforms like Azure DevOps and GitHub Actions.

---
## Course Context

**Course**: Deployment and DevOps  
**Section**: AI-Assisted CI/CD Pipeline Generation

This project demonstrates how modern AI development tools can accelerate DevOps workflows by generating CI/CD pipeline configurations. Using prompts and contextual instructions, AI can assist developers in producing YAML pipeline files that automate building, testing, and deploying applications. The focus of the project is not only on CI/CD automation itself but also on how AI can support developers in designing, refining, and optimizing pipeline configurations for platforms such as Azure DevOps and GitHub Actions.

---
## Concepts Demonstrated

- AI-assisted generation of CI/CD pipelines
- YAML pipeline configuration
- Automated build, test, and deployment workflows
- Azure DevOps pipeline automation
- GitHub Actions CI/CD workflows
- Dependency restoration and build automation
- Automated test execution within pipelines
- Secure deployment to Azure App Service
- Pipeline optimisation through AI suggestions

---
## Project Structure

- **RepoRoot/.azure/workflows** – Azure DevOps pipeline configuration for CI pipeline automation
- **RepoRoot/.github/workflows** – GitHub Actions workflow configuration for CI pipeline automation
- **scripts** – Deployment script used for provisioning infrastructure and deploying the application
- **src** – Minimal ASP.NET Core application used to trigger CI/CD pipelines

---
## How the Demonstration Works

1. A minimal ASP.NET Core application acts as the deployable artifact.
2. AI tools have been prompted to help generating CI/CD pipeline configurations.
3. YAML pipeline definitions are created for both:
   - GitHub Actions
   - Azure DevOps Pipelines
4. The pipelines automatically:
   - Restore dependencies
   - Build the application
   - Run tests
   - Package build artifacts
5. Deployment scripts provision infrastructure and deploy the application to Azure App Service.
6. Each code push triggers the automated pipeline, demonstrating a full CI/CD workflow.

---
## Real-World Relevance

Modern software teams rely on CI/CD pipelines to ensure consistent builds, automated testing, and reliable deployments. AI-assisted tooling can accelerate this process by helping developers generate pipeline templates, identify configuration errors, and optimise workflow stages. This project demonstrates how AI can be integrated into DevOps practices to reduce manual configuration effort while maintaining structured, version-controlled pipeline definitions.
