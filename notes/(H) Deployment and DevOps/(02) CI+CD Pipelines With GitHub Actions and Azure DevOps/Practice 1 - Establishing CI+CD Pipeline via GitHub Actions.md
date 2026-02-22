## Overview  

-	Create a new .NET web application  
-	Configure Git and GitHub integration  
-	Set up a GitHub Actions CI workflow  
-	Automate build and test processes  
-	Monitor and troubleshoot workflow execution  

---
## Step 1: Install Required Tools  

-	Visual Studio Code  
-	Install .NET SDK  
-	Install Git  
-	(Optional) Install GitHub CLI  
-	Verify installations:  
	 `dotnet --version`  
	 `git --version`  

---
## Step 2: Configure GitHub CLI  

-	Open terminal in VS Code (`Ctrl + ``)
-	Run authentication command:  
	 `gh auth login`  
-	Select:  
	-	GitHub.com  
	-	HTTPS protocol  
-	Authenticate via browser verification  
-	Confirm successful login  

---
## Step 3: Create a New .NET Web Application  

-	Create project:  
	 `dotnet new web -n MyWebApp`  
-	Navigate to project folder:  
	 `cd MyWebApp`  
-	Run application locally:  
	 `dotnet run`  
-	Verify app runs on `http://localhost:5000`  
-	Stop server with `Ctrl + C`  

---
## Step 4: Initialise Git and Connect to GitHub  

-	Set default branch:  
	 `git config --global init.defaultBranch main`  
-	Initialise repository:  
	 `git init`  
-	Create `.gitignore`:  
	 `dotnet new gitignore`  
-	Add and commit files:  
	 `git add .`  
	 `git commit -m "Initial commit - .NET web app"`  

### Create GitHub Repository  

-	Option 1 – GitHub CLI (Recommended):  
	`gh repo create MyWebApp --public --source=. --push`  

-	Option 2 – Manual Setup:  
	-	Create repository on GitHub  
	-	Add remote origin:  
		 `git remote add origin <repo-url>`  
	-	Rename branch and push:  
		 `git branch -M main`  
		 `git push -u origin main`  

---
## Step 5: Create GitHub Actions Workflow  

-	Create workflow directory:  
	-	`mkdir -p .github/workflows`  
-	Create workflow file:  
	-	`ci.yml` inside `.github/workflows/`  

### Configure Workflow  

-	Trigger:  
	-	Run on push events to `main` branch  
-	Runner:  
	-	Use Ubuntu environment  
-	Steps:  
	-	Set up .NET SDK  
	-	Restore dependencies  
	-	Build application  
	-	Run tests  

---
## Step 6: Commit and Trigger Workflow  

-	Add workflow file:  `git add .`  
-	Commit changes:  `git commit -m "Add GitHub Actions CI workflow"`  
-	Push to GitHub:     `git push origin main`  
-	Navigate to GitHub → Repository → Actions  
-	Verify workflow runs automatically  

---
## Step 7: Monitor and Troubleshoot  

-	Open GitHub → Repository → Actions  
-	Select workflow run  
-	Review execution logs  

### If Errors Occur  

-	Click failed step to inspect error message  
-	Modify `ci.yml` or application code  
-	Commit and push changes  
-	Workflow re-runs automatically on new commit  

---
## Outcome  

-	Local .NET web app connected to GitHub  
-	Automated CI workflow configured  
-	Build and tests executed on every push  
-	Continuous feedback loop established  
-	Foundation for full CI/CD pipeline implementation  