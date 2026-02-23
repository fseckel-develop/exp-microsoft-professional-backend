
- Objective:
	- Evaluate Copilot-generated deployment scripts
	- Identify strengths and weaknesses
	- Improve reliability, security, reusability, and maintainability
- Focus:
	- Structure and command order
	- Use of best practices
	- Production-readiness improvements

---
## Example 1: Azure App Deployment Script Review

- Scenario Overview:
	- Copilot-generated PowerShell script
	- Deploys Node.js app using Azure CLI
	- Creates resource group, App Service plan, and web app

### Original Script

```powershell
az group create `
  --name ContosoGroup `
  --location eastus

az appservice plan create `
  --name ContosoPlan `
  --resource-group ContosoGroup `
  --sku FREE

az webapp create `
  --name ContosoWebApp `
  --resource-group ContosoGroup `
  --plan ContosoPlan
````

### What the Script Does Well

- Correct dependency order: Resource group → App Service plan → Web app
- Valid Azure CLI syntax
- Cost-conscious SKU (FREE) for dev/test
- Simple and readable structure

### What Could Be Improved

- Hardcoded values (not reusable across environments)
- No runtime specification (Node version not defined)
- No conditional checks for existing resources
- No post-deployment verification
- No output of deployment endpoint
- No configuration settings (environment variables, startup command)

### Improved Script (Annotated)

```powershell
# Define variables for reuse
$RESOURCE_GROUP = "ContosoGroup"
$LOCATION = "eastus"
$PLAN = "ContosoPlan"
$APP_NAME = "ContosoWebApp"

# Create resource group
az group create 
  --name $RESOURCE_GROUP 
  --location $LOCATION

# Create App Service plan
az appservice plan create `
  --name $PLAN `
  --resource-group $RESOURCE_GROUP `
  --sku FREE

# Create web app with runtime specified
az webapp create `
  --name $APP_NAME `
  --resource-group $RESOURCE_GROUP `
  --plan $PLAN `
  --runtime "NODE|18-lts"

# Output the web app URL
az webapp show `
  --name $APP_NAME `
  --resource-group $RESOURCE_GROUP `
  --query defaultHostName -o tsv
```

### Rationale for Improvements

- Variables:
    - Improve reusability across environments
    - Reduce duplication and editing effort

- Runtime specification:
    - Ensures consistent execution environment

- Output command:
    - Confirms successful deployment
    - Provides endpoint for immediate validation

- Structured formatting:
    - Enhances readability and maintainability

---
## Example 2: Azure SQL Deployment Script Review

- Scenario Overview:
    - Copilot-generated script
    - Deploys Azure SQL Server and SQL Database
    - Requires security and configuration review

### Original Script

```powershell
az sql server create `
  --name ContosoSQL `
  --resource-group ContosoGroup `
  --location eastus `
  --admin-user adminuser `
  --admin-password SecureP@ssw0rd

az sql db create ` 
  --name ContosoDB `
  --resource-group ContosoGroup `
  --server ContosoSQL `
  --edition Basic
```

### What the Script Does Well

- Correct sequence: SQL Server created before database
- Valid Azure CLI syntax
- Clear and readable naming
- Explicit edition defined (Basic)

### What Could Be Improved

- Hardcoded credentials (security risk)
- No reusable variables
- No firewall rules configured
- No resource group creation
- No validation or existence checks
- No output confirmation
- No error handling

### Improved Script (Annotated)

```powershell
# Define reusable variables
$RESOURCE_GROUP = "ContosoGroup"
$LOCATION = "eastus"
$SQL_SERVER = "ContosoSQL"
$ADMIN_USER = "adminuser"
$DATABASE = "ContosoDB"

# Securely prompt for password
$ADMIN_PASSWORD = Read-Host -Prompt "Enter SQL admin password" -AsSecureString

# Create resource group
az group create `
  --name $RESOURCE_GROUP `
  --location $LOCATION

# Create SQL server
az sql server create `
  --name $SQL_SERVER `
  --resource-group $RESOURCE_GROUP `
  --location $LOCATION `
  --admin-user $ADMIN_USER `
  --admin-password $ADMIN_PASSWORD

# Configure firewall rule to allow Azure services
az sql server firewall-rule create `
  --resource-group $RESOURCE_GROUP `
  --server $SQL_SERVER `
  --name AllowAzureIPs `
  --start-ip-address 0.0.0.0 `
  --end-ip-address 0.0.0.0

# Create SQL database
az sql db create `
  --name $DATABASE `
  --resource-group $RESOURCE_GROUP `
  --server $SQL_SERVER `
  --service-objective Basic

# Output fully qualified domain name
az sql server show `
  --name $SQL_SERVER `
  --resource-group $RESOURCE_GROUP `
  --query fullyQualifiedDomainName -o tsv
```

### Rationale for Improvements

- Variables:
    - Enable environment flexibility (dev/test/prod)
    - Improve clarity and maintainability

- Secure password input:
    - Prevents credential exposure in source control

- Firewall configuration:
    - Ensures connectivity from Azure services

- Resource group creation:
    - Supports first-time environment setup

- Output command:
    - Confirms deployment success
    - Provides connection endpoint

- Structured layout:
    - Improves readability for team collaboration

---
## Key Takeaways

- Copilot-generated scripts provide:
    - Fast and functional deployment foundations
    - Correct dependency sequencing
    - Valid CLI syntax

- Production-readiness requires:
    - Parameterisation through variables
    - Secure credential handling
    - Environment validation logic
    - Monitoring and output confirmation
    - Improved maintainability structure

- Best Practice:
    - Treat Copilot output as a starting point
    - Review, optimise, and harden before production deployment