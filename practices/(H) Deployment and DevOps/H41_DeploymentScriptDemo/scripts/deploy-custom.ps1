# Define variables
$APP_NAME = "DeploymentScriptDemo"
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

# Publish Blazor WebAssembly App
dotnet publish ../src/DeploymentScriptDemo.csproj -c Release -o publish

# Package the wwwroot output
Compress-Archive `
    -Path publish/wwwroot/* `
    -DestinationPath publish/blazorapp.zip `
    -Force

# Deploy via ZIP
az webapp deploy `
    --name $APP_NAME `
    --resource-group $RESOURCE_GROUP `
    --src-path publish/blazorapp.zip `
    --type zip

# Output Application URL
az webapp show `
    --name $APP_NAME `
    --resource-group $RESOURCE_GROUP `
    --query defaultHostName `
    -o tsv