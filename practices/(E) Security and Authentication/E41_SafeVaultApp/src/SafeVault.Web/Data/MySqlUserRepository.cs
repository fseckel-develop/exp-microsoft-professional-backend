using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SafeVault.Web.Configuration;
using SafeVault.Web.Models;

namespace SafeVault.Web.Data;

public sealed class MySqlUserRepository : IUserRepository
{
    private readonly string _connectionString;

    public MySqlUserRepository(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public async Task<bool> UserExistsAsync(string username, CancellationToken ct = default)
    {
        const string sql = "SELECT COUNT(*) FROM Users WHERE Username = @username";

        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(ct);

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", username);

        var result = await command.ExecuteScalarAsync(ct);
        var count = Convert.ToInt64(result);

        return count > 0;
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
    {
        const string sql = """
            SELECT Id, Username, Email, PasswordHash, Role
            FROM Users
            WHERE Username = @username
            LIMIT 1
            """;

        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(ct);

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", username);

        await using var reader = await command.ExecuteReaderAsync(ct);

        if (!await reader.ReadAsync(ct))
            return null;

        var idOrdinal = reader.GetOrdinal("Id");
        var usernameOrdinal = reader.GetOrdinal("Username");
        var emailOrdinal = reader.GetOrdinal("Email");
        var passwordHashOrdinal = reader.GetOrdinal("PasswordHash");
        var roleOrdinal = reader.GetOrdinal("Role");

        return new User
        {
            Id = reader.GetInt32(idOrdinal),
            Username = reader.GetString(usernameOrdinal),
            Email = reader.GetString(emailOrdinal),
            PasswordHash = reader.GetString(passwordHashOrdinal),
            Role = reader.GetString(roleOrdinal)
        };
    }

    public async Task<User> InsertUserAsync(User user, CancellationToken ct = default)
    {
        const string sql = """
            INSERT INTO Users (Username, Email, PasswordHash, Role)
            VALUES (@username, @email, @passwordHash, @role);
            SELECT LAST_INSERT_ID();
            """;

        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(ct);

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", user.Username);
        command.Parameters.AddWithValue("@email", user.Email);
        command.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
        command.Parameters.AddWithValue("@role", user.Role);

        var result = await command.ExecuteScalarAsync(ct);
        user.Id = Convert.ToInt32(result);

        return user;
    }
}