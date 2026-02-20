Write-Host "Prerequisites: PowerShell, Azure CLI, and an active Azure subscription"

Write-Host "Step 1: Log in to Azure and verify active subscription"
az login
az account show

Write-Host "Step 2: Register Resource Provider (if not already registered)"
az provider register --namespace Microsoft.Web
az provider show -n Microsoft.Web

Write-Host "Step 3: Create Resource Group and App Service Plan"
az group create --name MyResourceGroup --location westeurope

az appservice plan create `
    --name MyAppServicePlan `
    --resource-group MyResourceGroup `
    --sku F1

Write-Host "Step 4: Deploy the Application from /src"
az webapp up `
    --name AzureDeploymentDemo `
    --resource-group MyResourceGroup `
    --plan MyAppServicePlan `
    --location westeurope `
    --src-path ../src