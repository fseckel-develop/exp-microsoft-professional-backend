# (H21) GitHubActionsDemo

A minimal ASP.NET Core application used to demonstrate how GitHub Actions can automatically build and test a project using a CI/CD workflow triggered by repository events.

---
## Course Context

**Course**: Deployment and DevOps  
**Section**: CI/CD Automation with GitHub Actions

This project illustrates how a CI/CD pipeline can be configured directly in a GitHub repository. It demonstrates how workflows defined in YAML automatically trigger builds and tests when code is pushed or pull requests are created.

---
## Concepts Demonstrated

- Continuous Integration (CI) pipelines
- GitHub Actions workflows
- YAML-based workflow configuration
- Repository event triggers (push and pull requests)
- Automated build and test execution
- CI pipeline failure simulation
- Basic .NET pipeline automation

---
## Project Structure

- **RepoRoot/.github/workflows** – GitHub Actions workflow configuration for CI pipeline automation
- **src/Program.cs** – Minimal ASP.NET Core application used to trigger CI workflows