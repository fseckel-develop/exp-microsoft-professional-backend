# (H23) AlertOnFailureDemo

A minimal CI/CD monitoring demo that shows how pipeline failure notifications can be configured in both Azure DevOps and GitHub Actions, using a small ASP.NET Core app as the trigger source.

---
## Course Context

**Course**: Deployment and DevOps 
**Section**: CI/CD Monitoring, Alerts, and Pipeline Maintenance

This project demonstrates how CI/CD platforms can react to failed builds by sending automated notifications. It highlights the monitoring side of DevOps, showing how build pipelines can be extended beyond restore/build/test steps to include alerting and operational response.

---
## Concepts Demonstrated

- CI/CD pipeline monitoring
- Failure-triggered notifications
- Multi-stage Azure DevOps pipelines
- Conditional execution with failed-build handling
- GitHub Actions failure alert workflow
- Email-based pipeline alerts
- Build/test automation in .NET projects
- Operational visibility and pipeline maintenance practices

---
## Project Structure

- **RepoRoot/.azure** – Azure DevOps pipeline configuration, including build and failure-notification stages
- **RepoRoot/.github** – GitHub Actions workflow configuration for build validation and alert-on-failure automation
- **src/Program.cs** – Minimal ASP.NET Core demo application used to trigger pipeline runs and simulate failure scenarios