
- Use Copilot to identify and correct errors in a flawed deployment script.
- Simulate real-world debugging by:
	- Reviewing a buggy script
	- Using Copilot prompts to identify issues
	- Applying optimisations for reliability, clarity, and reusability
- Copilot assists with:
	- Detecting missing arguments
	- Correcting misordered commands
	- Optimising performance
	- Refactoring for clarity and reusability
- Debugging deployment scripts involves more than fixing errors 
  — it improves script performance, clarity, and maintainability

---
## Example 1: Debugging a Flawed Azure Deployment Script

### Scenario

- Received a PowerShell script intended to create a resource group and deploy a VM.
- Script contains syntax errors and poor structure.

### Original Script

```powershell
az group create --name MyResourceGroup --location
az vm create --resource-group MyResourceGroup --name MyVM --image UbuntuLTS
az configure --defaults group=MyResourceGroup location=eastus
````

### Problems Identified

- First command missing `--location` argument
- `az` configure executed too late
- Hardcoded strings reduce reusability
- No logic for secure or cost-effective deployment

### Copilot Suggestions

- Add a value for `--location` (e.g., `eastus`)
- Move `az` configure to the top so defaults apply early
- Add SSH key generation and free-tier VM size to `az vm create`

### Corrected Script

```powershell
# Set default values for location and group
az configure --defaults group=MyResourceGroup location=eastus

# Create a resource group in the default location
az group create --name MyResourceGroup --location eastus

# Create a virtual machine with free-tier settings and SSH key setup
az vm create `
  --name MyVM `
  --image UbuntuLTS `
  --size Standard_B1s `
  --admin-username azureuser `
  --generate-ssh-keys
```

### Refactored Version for Reusability

```powershell
# Define reusable variables
$resourceGroup = "MyResourceGroup"
$location = "eastus"
$vmName = "MyVM"

# Set defaults
az configure --defaults group=$resourceGroup location=$location

# Create the resource group
az group create --name $resourceGroup --location $location

# Create a VM
az vm create `
  --name $vmName `
  --image UbuntuLTS `
  --size Standard_B1s `
  --admin-username azureuser `
  --generate-ssh-keys
```

### Key Outcomes

- Copilot detected missing values and syntax errors
- Suggested proper command order to avoid mistakes
- Optimised performance with free-tier resources
- Refactored script for maintainability and portability
- Final script is easier to debug, update, and reuse

---
## Example 2: Debugging a Flawed Azure SQL Deployment Script

### Scenario

- Received a PowerShell script to deploy an Azure SQL database
- Script does not run correctly

### Original Script

```powershell
az sql server create --name MySqlServer `
  --resource-group `
  --admin-user adminuser `
  --admin-password MyStrongP@ssword123
az sql db create `
  --name MyDatabase `
  --server MySqlServer `
  --edition Basic
```

### Problems Identified

- Missing resource group creation
- `--location` argument missing for SQL server
- Hardcoded values reduce maintainability
- Deprecated `--edition` parameter used
- Command sequence is illogical (server before resource group)

### Copilot Suggestions

- Add resource group creation step
- Add missing `--location` argument
- Replace hard-coded strings with variables
- Replace `--edition` with `--service-objective Basic`
- Reorder commands for logical flow

### Corrected Script

```powershell
# Define reusable variables
$resourceGroup = "MyResourceGroup"
$location = "eastus"
$sqlServer = "MySqlServer"
$adminUser = "adminuser"
$adminPassword = "MyStrongP@ssword123"
$database = "MyDatabase"

# Create the resource group
az group create --name $resourceGroup --location $location

# Create the SQL server
az sql server create `
  --name $sqlServer `
  --resource-group $resourceGroup `
  --location $location `
  --admin-user $adminUser `
  --admin-password $adminPassword

# Create the SQL database
az sql db create `
  --name $database `
  --server $sqlServer `
  --resource-group $resourceGroup `
  --service-objective Basic
```

### Key Outcomes

- Added missing resource group creation step
- Fixed missing `--location` argument
- Replaced hard-coded values with variables for maintainability
- Updated deprecated `--edition` parameter to `--service-objective Basic`
- Reordered commands for logical execution flow
- Final script is easier to debug, update, and reuse across environments