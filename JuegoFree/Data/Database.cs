using MySql.Data.MySqlClient;

namespace JuegoFree.Data
{
    public static class Database
    {
        private static string connectionString =
            "server=localhost;database=juegofree;uid=root;pwd=admin;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
