
- Objective  
	- Use Azure Automation to create and configure a Runbook  
	- Automate a simple maintenance task  
	- Test, publish, and simulate scheduling  

- Purpose  
	- Reduce manual operational work  
	- Improve reliability and consistency  
	- Practice real-world automation workflow  

---
## Example 1: Runbook to Shut Down Virtual Machines

### Step 1: Create an Automation Account

- In the Azure portal:  
	- Search for **Automation Accounts**  
	- Click **Create**  
	- Select subscription and resource group  
	- Name the account (e.g., `DevMaintenanceAutomation`)  
	- Choose region  
	- Click **Review + Create**, then **Create**  

- This Automation Account will store and manage all Runbooks  

### Step 2: Create a Runbook

- Navigate to **Runbooks**  
- Click **Create a Runbook**  
- Name: `ShutDownVMs`  
- Runbook type: **PowerShell**  
- Click **Create** to open the editor  

### Step 3: Write the Script

- PowerShell script to shut down all running VMs in a resource group:

```powershell
param (
    [string]$resourceGroupName = "myResourceGroup"
)

$vmList = Get-AzVM -ResourceGroupName $resourceGroupName -Status

foreach ($vm in $vmList) {
    if ($vm.PowerState -eq "VM running") {
        Stop-AzVM -ResourceGroupName $vm.ResourceGroupName -Name $vm.Name -Force
        Write-Output "Stopped VM: $($vm.Name)"
    }
}
````

- Script behaviour
    - Accepts resource group as parameter
    - Retrieves all VMs in that group
    - Checks power state
    - Stops only running VMs
    - Outputs execution logs

### Step 4: Test the Runbook

- Click **Test Pane**
- Provide resource group name
- Run script
- Verify:
    - Correct VMs are targeted
    - No syntax errors
    - Expected output appears

### Step 5: Publish and Schedule

- Click **Publish**
- Navigate to **Schedules → Add a Schedule**
- Create schedule (e.g., daily at 10 PM)
- Link schedule to Runbook
- Result: Runbook executes automatically at defined intervals

### Step 6: Monitor Runbook Execution

- Go to **Jobs** tab
- Review:
    - Job status
    - Output logs
    - Errors or warnings
- Use logs for troubleshooting and verification

### What You Achieved

- Automated VM shutdown process
- Parameterised script for reuse
- Scheduled execution
- Built-in logging and monitoring
- Reduced manual effort and operational risk

---
## Example 2: Runbook for Logging Maintenance 

### Step 1: Set Up an Automation Account

- In Azure portal:
    - Search **Automation Accounts**
    - Create a new account

### Step 2: Create a New Runbook

- Navigate to **Runbooks → Create**
- Name: GenerateLogs (or similar)
- Type: **PowerShell**

### Step 3: Write a Basic Script

- Example maintenance simulation script:

```powershell
Write-Output "Maintenance task started at $(Get-Date)"

Start-Sleep -Seconds 5

Write-Output "Performing simulated maintenance..."

Start-Sleep -Seconds 3

Write-Output "Maintenance task completed at $(Get-Date)"
```

- Script behaviour
    - Logs start timestamp
    - Simulates work with delay
    - Logs completion timestamp

### Step 4: Test the Runbook

- Open **Test Pane**
- Execute script
- Confirm output appears correctly
- Ensure no syntax errors

### Step 5: Publish and Schedule the Runbook

- Click **Publish**
- Create schedule (hourly, daily, etc.)
- Link schedule to Runbook
- Automation now runs without manual intervention

### Step 6: Monitor Execution Logs

- Go to **Jobs** tab
- Review:
    - Execution status
    - Output messages
    - Errors (if any)
- Verify:
    - Script runs as scheduled
    - Logs display expected timestamps and messages

---
## Key Takeaways

- Azure Automation Runbooks enable repeatable, script-based maintenance
- PowerShell scripts automate infrastructure tasks
- Scheduling ensures consistent execution
- Built-in job logging supports transparency and troubleshooting
- Automation reduces human error and improves operational efficiency