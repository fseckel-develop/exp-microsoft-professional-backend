## Overview  

- Explore built-in monitoring features in GitHub Actions and Azure DevOps  
- Analyse dashboards, logs, and run summaries  
- Simulate alert configurations for pipeline failures  
- Compare monitoring capabilities of both platforms  
- Reflect on suitability for production environments  

---
## 1. Exploring Monitoring Dashboards  

### GitHub Actions  

- Navigate to repository → **Actions** tab  
- View list of recent workflow runs  
	- Status indicators (✅ success / ❌ failure)  
	- Branch and commit reference  
	- Execution duration  
- Open a workflow run to inspect:  
	- Job breakdown  
	- Step-by-step logs  
	- Error messages with stack traces  
- Strengths observed:  
	- Clean, minimal UI  
	- Easy navigation between jobs and steps  
	- Fast identification of failing step  

### Azure DevOps  

- Go to project → **Pipelines** → **Runs**  
- View recent pipeline executions  
	- Status indicators (✅ success / ❌ failure)   
	- Trigger source  
	- Duration  
- Open a run → **Summary tab**  
	- Stage view (Build / Deploy separation)  
	- Test results overview  
	- Artefacts  
	- Linked work items (if configured)  
- Access detailed logs per job  
- Strengths observed:  
	- Clear stage visualisation  
	- Built-in test reporting  
	- Stronger enterprise-level overview  

---
## 2. Simulating a Simple Alert System  

### GitHub Actions – Alert Configuration  

- GitHub does not provide advanced built-in failure subscriptions like Azure DevOps  
- Alerts can be implemented via workflow steps  
- Common approach:  
	- Use conditional execution (`if: failure()`)  
	- Send notification via Slack, Discord, or Email

#### Example YAML Snippet (GitHub Actions)

```yaml
name: CI Pipeline
	
on:
  push:
    branches: [ main ]
	
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout code
        uses: actions/checkout@v4
		
      - name: Run build
        run: npm run build
		
      - name: Notify on failure
        if: failure()
        uses: slackapi/slack-github-action@v1
        with:
          payload: |
            {
              "text": "Pipeline failed on main branch."
            }
```

- Alert triggers only if previous steps fail
- Requires webhook or external service configuration
- Highly flexible but requires manual setup

### Azure DevOps – Alert Configuration

- Go to **Project Settings** → **Notifications**
- Select **New subscription**
- Available alert types include:
    - Pipeline failure
    - Deployment completion or failure
    - Test failure
    - Manual approval pending
- Define:
    - Event trigger
    - Scope (specific pipeline)
    - Recipients (users or groups)

#### Example YAML Condition for Failure Handling (Azure DevOps)

```YAML
stages:
- stage: Build
  jobs:
    - job: BuildJob
      steps:
        - script: npm run build
	
- stage: Notify
  condition: failed()
  jobs:
    - job: Alert
      steps:
        - script: echo "Pipeline failed. Triggering alert."
```

- Alerts can also be configured without modifying YAML
- Built-in enterprise notification system
- Less need for external integrations

---
## 3. Platform Comparison

### Ease of Understanding Pipeline Status

- GitHub Actions:
    - Very intuitive and minimal
    - Fast identification of failing step
    - Best for small to medium projects

- Azure DevOps:
    - More structured stage overview
    - Better visualisation for multi-stage pipelines
    - Stronger summary and test integration

- **Conclusion:**
	- GitHub Actions is slightly easier for quick inspection
	- Azure DevOps provides deeper structured insight

### Flexibility and Visibility for Alerting

- GitHub Actions:
    - Highly customisable
    - Requires manual YAML configuration
    - Depends on third-party integrations

- Azure DevOps:
    - Built-in notification subscriptions
    - Easier enterprise-level alert management
    - Granular event-based triggers

- **Conclusion:**
	- Azure DevOps provides more built-in alerting capabilities
	- GitHub offers more customisation flexibility

### Preferred Tool for Production Monitoring

- For small teams or open-source projects:
    - GitHub Actions
    - Simple setup, integrated with repository

- For enterprise production environments:
    - Azure DevOps
    - Stronger more robust monitoring dashboards
    - Built-in notification system
    - Better stage visualisation and governance features
    - Stronger support for multi-stage deployments

---
## Outcome

- Understanding of monitoring dashboards in both platforms
- Ability to interpret logs, failures, and pipeline summaries
- Knowledge of alert configuration methods
- Comparison of flexibility and enterprise readiness
- Clear evaluation of monitoring strengths in GitHub Actions vs. Azure DevOps
