using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SafeVault.Web.Configuration;

namespace SafeVault.Web.Infrastructure;

public sealed class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public async Task InitializeAsync(CancellationToken ct = default)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(ct);

        const string createUsersTableSql = """
            CREATE TABLE IF NOT EXISTS Users (
                Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                Username VARCHAR(50) NOT NULL UNIQUE,
                Email VARCHAR(100) NOT NULL,
                PasswordHash VARCHAR(255) NOT NULL,
                Role VARCHAR(20) NOT NULL
            );
            """;

        await using var command = new MySqlCommand(createUsersTableSql, connection);
        await command.ExecuteNonQueryAsync(ct);
    }
}