## Overview

- Objective:  
	- Create a small web application using Visual Studio Code  
	- Deploy it to Azure App Service  
	- Verify successful cloud deployment  
- Focus areas:  
	- Azure CLI usage  
	- Resource provisioning  
	- Cloud deployment workflow  

---
## Prerequisites

- Azure CLI installed (`brew install azure-cli`)  
- Active Azure account (via Azure Portal)  
- Visual Studio Code installed  
- .NET SDK installed  

---
## Step 1: Initialise the Web Application

#### Create a New Blazor WebAssembly Project

- Run in VS Code terminal:  `dotnet new blazorwasm -o <YourAppName>`  
- Navigate into the project folder  
- Open Integrated Terminal (`Ctrl + ~`)  

#### Authenticate with Azure

- Log in:  `az login`  
- Verify active subscription:  `az account show`  

---
## Step 2: Create Azure Infrastructure

#### Register Resource Provider (if required)

- Run:  `az provider register --namespace Microsoft.Web`  

#### Create a Resource Group

- Logical container for Azure resources 
- Command:  
  `az group create --name <YourResourceGroupName> --location <YourLocation>  

#### Create an App Service Plan

- Defines pricing tier and compute resources  
- F1 = Free tier (suitable for testing)  
- Command:  
  `az appservice plan create --name <YourAppServicePlanName>                                                --resource-group <YourResourceGroupName>                                       --sku F1                                             `  
#### Create the Web App (App Service Instance)

- Provisions hosting environment for the application  
- Command:  
  `az webapp create --name <YourAppName>                                                           --resource-group <YourResourceGroupName>                                       --plan <YourAppServicePlanName>                               `  
---
## Step 3: Deploy the Application

- Navigate to project directory  
- Run:  `az webapp up --name <app-name>`  
- Azure CLI:  
	- Builds the application  
	- Packages the project  
	- Deploys to App Service  
- Output provides the public application URL  

---
## Step 4: Verify Deployment

- Locate the public URL in terminal output  
- Open the URL in a web browser  
- Confirm:  
	- Web interface loads correctly (Blazor app)  
	- No runtime errors  
- If issues occur:  
	- Check logs using Azure CLI  
	- Verify correct resource names and subscription  

---
## Brief Deployment Report

### Purpose

- Demonstrate end-to-end deployment workflow  
- Practice provisioning Azure infrastructure  
- Deploy a locally developed web application to the cloud  

### Steps Followed

- Created Blazor WebAssembly project  
- Logged into Azure via Azure CLI  
- Registered required provider  
- Created resource group and App Service plan  
- Provisioned Azure Web App  
- Deployed project using `az webapp up`  
- Verified deployment via public URL  

---
## Key Learning Outcomes

- Understand Azure App Service provisioning workflow  
- Use Azure CLI for infrastructure and deployment  
- Deploy .NET applications to Azure  
- Validate and troubleshoot cloud deployments  