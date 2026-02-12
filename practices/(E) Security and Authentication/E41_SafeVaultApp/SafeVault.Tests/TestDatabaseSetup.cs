using MySql.Data.MySqlClient;

namespace SafeVault.Tests
{
    public static class TestDatabaseSetup
    {
        private const string ServerConnection = "server=localhost;user=root;password=My$tr0ngP@ssw0rd123;";
        private const string DatabaseName = "safevault";
        private const string TestUserConnection = "server=localhost;database=safevault;user=testuser;password=My$tr0ngP@ssw0rd123;";

        // Call this before running any tests
        public static void Initialize()
        {
            CreateDatabaseIfNotExists();
            CreateUsersTableIfNotExists();
        }

        private static void CreateDatabaseIfNotExists()
        {
            using var connection = new MySqlConnection(ServerConnection);
            connection.Open();

            string createDbSql = $"CREATE DATABASE IF NOT EXISTS {DatabaseName};";
            using var command = new MySqlCommand(createDbSql, connection);
            command.ExecuteNonQuery();
        }

        private static void CreateUsersTableIfNotExists()
        {
            using var connection = new MySqlConnection(TestUserConnection);
            connection.Open();

            string createTableSql = @"
                CREATE TABLE IF NOT EXISTS users (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Username VARCHAR(255) NOT NULL,
                    Email VARCHAR(255) NOT NULL,
                    PasswordHash VARCHAR(255) NOT NULL,
                    Role VARCHAR(50) NOT NULL
                );
            ";

            using var command = new MySqlCommand(createTableSql, connection);
            command.ExecuteNonQuery();
        }
    }
}