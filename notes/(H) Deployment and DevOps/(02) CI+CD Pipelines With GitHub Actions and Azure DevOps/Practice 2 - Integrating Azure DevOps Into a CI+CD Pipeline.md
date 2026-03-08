## Overview  

- Create a simple Node.js application  
- Initialise a local Git repository  
- Push the project to GitHub  
- Create an Azure DevOps organization and project  
- Connect Azure DevOps to GitHub  
- Create and configure a YAML-based pipeline  
- Trigger and monitor automated CI/CD execution

---
## Step 1: Create a Simple Node.js Application  

- Open Visual Studio Code  
- Install required tools (if not already installed):  
	- Node.js (from nodejs.org)  
	- Git (from git-scm.com)  
- Create and navigate into project folder:  
	 `mkdir ci-cd-demo`  
	 `cd ci-cd-demo`  
- Initialise Node.js project:  
	 `npm init -y`  
	 Creates `package.json` (required project configuration file)  
- Create application entry file:  
	 `touch index.js`  
- Add simple JavaScript code to `index.js`:  
	 `console.log("Hello, CI/CD Pipeline!");`  
- Save the file  

---
## Step 2: Initialise Git and Push to GitHub  

### Local Repository Setup  

- Initialise Git repository:  
	 `git init`  
- Create `.gitignore` file:  
	 `echo "node_modules/" > .gitignore`  
- Stage project files:  
	 `git add .`  
- Commit changes:  
	 `git commit -m "Initial commit - simple Node.js app"`  

### Create GitHub Repository  

- Go to GitHub  
- Click **New Repository**  
- Set repository name (e.g., `ci-cd-demo`)  
- Choose visibility (Public or Private)  
- Do NOT initialise with a README  
- Click **Create Repository**  

### Connect and Push to GitHub  

- Add remote origin:  
	 `git remote add origin https://github.com/GITHUB_USERNAME/ci-cd-demo.git`  
- Rename branch to main:  
	 `git branch -M main`  
- Push code to GitHub:  
	 `git push -u origin main`  
- Project is now hosted on GitHub  

---
## Step 3: Create an Azure DevOps Organization and Project  

### Create Organization  

- Go to: https://aex.dev.azure.com  
- Sign in with Microsoft account  
- Click **Create new organisation** (if none exists)  
- Choose:  
	- Unique organisation name (e.g., yourname-devops)  
	- Region (closest geographic location)  
- Click **Continue**  

### Create Project  

- From Azure DevOps dashboard, click **New Project**  
- Enter project name (e.g., `CI-CD-Demo`)  
- Set visibility (Public or Private)  
- Click **Create**  

---
## Step 4: Connect Azure DevOps to GitHub  

- In Azure DevOps project, go to **Project Settings**  
- Under Pipelines, select **Service connections**  
- Click **New Service Connection**  
- Choose **GitHub**  
- Click **Authorise Azure DevOps**  
- Select repository (`ci-cd-demo`)  
- Click **Save** 

---
## Step 5: Setting up Self-Hosted Agent (MacOS)

- Go to your Azure DevOps Project → Project Settings → Agent Pools → Default → New Agent
- Download the Agent
- Create a proper folder for the Agent
	  `mkdir ~/.azure/agent`
	  `cd ~/.azure/agent`
- Move the downloaded agent `.tar` file into this folder
	- Unpack the agent:
		  `tar xvf vsts-agent-osx-x64-4.268.0.tar`
	- Expected files after extraction: `config.sh`, `run.sh`, `bin/`
- Remove macOS quarantine to allow execution:
	 `xattr -r -d com.apple.quarantine .`

- Create a Personal Access Token (PAT) in Azure DevOps
	- Go to: User Settings → Personal Access Tokens → New Token
	- Permissions:
	    - Agent Pools (Read & manage)
	    - Build (Read & execute)
	- Copy the generated token

- Configure the agent running `./config.sh`
	- Enter when prompted:
		- **Server URL:** `https://dev.azure.com/YOUR_ORG_NAME`
		- **Authentication type:** `PAT`
		- Paste your token
		- **Agent Pool:** `Default`
		- **Agent Name:** `Franz-Mac-Agent` (or any descriptive name)
		- Press Enter for default `_work` folder

- Test run the agent
	 `./run.sh`
	 Should display: `Listening for Jobs`

- Install agent as a background service for auto-start
	  `sudo ./svc.sh install`
	  `sudo ./svc.sh start`
	- Check status: `sudo ./svc.sh status`

---
## Step 6: Add and Upload the YAML Pipeline File  

- Navigate to local project:  
	 `cd ci-cd-demo`  
- Create pipeline file in root directory:  
	 `touch .azure-pipelines.yml`  
- Open file in VS Code  
- Add pipeline workflow configuration  
- to use self-hosted agent, include `pool: name: Default`
- Save the file  

---
## Step 7: Create the Azure Pipeline 

- If using free tier:  
	- Request free parallelism grant (if prompted)  
- Go to Azure DevOps → `CI-CD-Demo` project  
- Navigate to **Pipelines > New Pipeline**  
- Select source:  
	- GitHub  
	- Authorise if prompted  
- Select repository:  
	- `ci-cd-demo`  
- Choose configuration type:  
	- Existing YAML file  
- Set YAML path:  
	- `/.azure-pipelines.yml`  (or other)
- Click **Continue**  
- Click **Run** to create initial pipeline  

---
## Step 8: Push Pipeline Configuration to GitHub  

- Stage changes:
	 `git add .`  
- Commit changes:
	 `git commit -m "<CommitMessage"`  
- Push changes:
	 `git push`  
- This automatically triggers the Azure DevOps pipeline  

---
## Step 9: Monitor Pipeline Execution  

- Go to Azure DevOps  
- Navigate to **Pipelines > Runs**  
- Select the active run  
- View execution logs  
- Build result indicators:  
	- Green checkmark → Success  
	- Error logs → Debug and fix issues  

---
## Outcome  

- Working Node.js project hosted on GitHub  
- Connected Azure DevOps project  
- YAML-based CI/CD pipeline configured  
- Automatic build trigger on every push to main  
- Ability to monitor, debug, and manage pipeline runs  
- Practical understanding of GitHub–Azure DevOps integration in a CI/CD workflow  