param (
    [string]$ConfigFile = "appsettings.Development.json",
    [string]$RootUser = "root",
    [string]$RootPassword = "",
    [string]$DbContext = "CoffeeShopDbContext"
)

Write-Host "Reading configuration from $ConfigFile..."

if (!(Test-Path $ConfigFile)) {
    Write-Error "Config file not found."
    exit 1
}

# Read JSON
$config = Get-Content $ConfigFile -Raw | ConvertFrom-Json
$connectionString = $config.ConnectionStrings.MySql

if (-not $connectionString) {
    Write-Error "ConnectionStrings:MySql not found."
    exit 1
}

# Parse connection string
$parts = @{}
$connectionString.Split(';') | ForEach-Object {
    if ($_ -match '=') {
        $key, $value = $_ -split '=', 2
        $parts[$key.Trim()] = $value.Trim()
    }
}

$server   = $parts["Server"]
$database = $parts["Database"]
$appUser  = $parts["User"]
$appPass  = $parts["Password"]

if (-not $server -or -not $database -or -not $appUser -or -not $appPass) {
    Write-Error "Connection string missing required parts."
    exit 1
}

Write-Host "Creating database '$database' on server '$server'..."

$sql = @"
CREATE DATABASE IF NOT EXISTS $database;
CREATE USER IF NOT EXISTS '$appUser'@'localhost' IDENTIFIED BY '$appPass';
GRANT ALL PRIVILEGES ON $database.* TO '$appUser'@'localhost';
FLUSH PRIVILEGES;
"@

$mysqlArgs = @("--host=$server", "--user=$RootUser", "--execute=$sql")
if ($RootPassword -ne "") { $mysqlArgs += "--password=$RootPassword" }

& mysql @mysqlArgs
if ($LASTEXITCODE -ne 0) { Write-Error "Database setup failed."; exit 1 }

Write-Host "Database ready."

Write-Host "Running EF Core migrations..."

# Ensure we run where the .csproj is (you already run from src, but this is safe)
$here = Get-Location
$csproj = Get-ChildItem -Path $here -Filter *.csproj | Select-Object -First 1

if (-not $csproj) {
    Write-Error "No .csproj found in $here. Run the script from the folder that contains the project file."
    exit 1
}

Write-Host "Using project: $($csproj.Name)"

dotnet restore $csproj.FullName
if ($LASTEXITCODE -ne 0) { Write-Error "dotnet restore failed."; exit 1 }

Write-Host "Ensuring migrations exist..."
dotnet ef migrations add InitialCreate --context $DbContext --output-dir Data/Migrations --project $csproj.FullName --startup-project $csproj.FullName 2>$null

Write-Host "Running EF Core migrations..."
dotnet ef database update --context $DbContext --project $csproj.FullName --startup-project $csproj.FullName

if ($LASTEXITCODE -ne 0) { Write-Error "EF migration failed."; exit 1 }
