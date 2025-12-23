
```shell
# Install EF Core tools globally
dotnet tool install --global dotnet-ef
```

```shell
# Create a new console application
dotnet new console -n EFCoreModelApp
cd EFCoreModelApp
```

```shell
# Install EF Core SQLite and tools
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

```shell
# When using MySQL
dotnet add package Pomelo.EntityFrameworkCore.MySql 
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.2 
dotnet new tool-manifest  
dotnet tool install dotnet-ef
```

```shell
# Test the setup
dotnet run
```


Then Define all Models in /Models Directory and DbContext in the project root:

```shell
# Add initial migration
dotnet ef migrations add InitialCreate
```

```shell
# Apply the migration to create the database
dotnet ef database update
```