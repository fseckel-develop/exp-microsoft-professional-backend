param (
    [string]$Server = "localhost",
    [string]$Database = "safevault",
    [string]$AppUser = "testuser",
    [string]$AppPassword = 'My$tr0ngP@ssw0rd123',
    [string]$RootUser = "root",
    [string]$RootPassword = ""
)

Write-Host "Setting up MySQL database '$Database' on server '$Server'..."

$sql = @"
CREATE DATABASE IF NOT EXISTS $Database;

CREATE USER IF NOT EXISTS '$AppUser'@'localhost' IDENTIFIED BY '$AppPassword';

GRANT ALL PRIVILEGES ON $Database.* TO '$AppUser'@'localhost';

FLUSH PRIVILEGES;

USE $Database;

CREATE TABLE IF NOT EXISTS Users (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) NOT NULL
);
"@

$mysqlArgs = @("--host=$Server", "--user=$RootUser", "--execute=$sql")

if ($RootPassword -ne "") {
    $mysqlArgs += "--password=$RootPassword"
}

& mysql @mysqlArgs

if ($LASTEXITCODE -ne 0) {
    Write-Error "Database setup failed."
    exit 1
}

Write-Host "Database, user, and Users table are ready."