# Define variables
$APP_NAME = "PipelineGenerationDemo"
$RESOURCE_GROUP = "MyResourceGroup"
$LOCATION = "westeurope"
$PLAN_NAME = "MyAppServicePlan"

# Login to Azure
az login

# Create Resource Group
az group create `
    --name $RESOURCE_GROUP `
    --location $LOCATION

# Create App Service Plan (Linux)
az appservice plan create `
    --name $PLAN_NAME `
    --resource-group $RESOURCE_GROUP `
    --sku FREE `
    --is-linux

# Create Web App with .NET 8 runtime
az webapp create `
    --name $APP_NAME `
    --resource-group $RESOURCE_GROUP `
    --plan $PLAN_NAME `
    --runtime "DOTNET:8"

# Determine script-relative paths
$publishDir = Join-Path $PSScriptRoot 'publish'
$projectPath = Join-Path $PSScriptRoot '..\src\PipelineGenerationDemo.csproj'

# Publish Web App
dotnet publish $projectPath -c Release -o $publishDir

# Package the published files (for ZIP deploy)
Compress-Archive `
    -Path "$publishDir/*" `
    -DestinationPath (Join-Path $publishDir 'app.zip') `
    -Force

# Deploy via ZIP
$zipPath = Join-Path $publishDir 'app.zip'
az webapp deploy `
    --name $APP_NAME `
    --resource-group $RESOURCE_GROUP `
    --src-path $zipPath `
    --type zip

# Output Application URL
az webapp show `
    --name $APP_NAME `
    --resource-group $RESOURCE_GROUP `
    --query defaultHostName `
    -o tsv