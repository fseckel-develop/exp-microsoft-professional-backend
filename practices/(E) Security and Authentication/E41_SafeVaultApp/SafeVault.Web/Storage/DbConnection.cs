using MySql.Data.MySqlClient;

namespace SafeVault.Web.Storage
{
    public static class Database
    {
        private static string connectionString =
            "Server=localhost;Database=safevault;Uid=testuser;Pwd=My$tr0ngP@ssw0rd123;";

        public static bool UserExists(string username)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public static void InsertUser(string username, string email, string passwordHash, string role)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = @"
                INSERT INTO Users (Username, Email, PasswordHash, Role)
                VALUES (@username, @email, @passwordHash, @role)
            ";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@passwordHash", passwordHash);
            command.Parameters.AddWithValue("@role", role);

            command.ExecuteNonQuery();
        }

        public static string? GetPasswordHash(string username)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            string query = "SELECT PasswordHash FROM Users WHERE Username = @username";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            var result = cmd.ExecuteScalar();
            return result?.ToString();
        }

        public static string? GetUserRole(string username)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            string query = "SELECT Role FROM Users WHERE Username = @username";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            var result = cmd.ExecuteScalar();
            return result?.ToString();
        }
    }
}
