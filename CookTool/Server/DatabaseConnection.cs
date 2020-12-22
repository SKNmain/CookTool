using NPoco;

namespace CookTool.Server
{
    public class DatabaseConnection
    {
        private static readonly DatabaseConnection instance = new DatabaseConnection();
        public IDatabase database;
        private string connectionString = @"Server=127.0.0.1;Port=3306;database=cooktool;user id=root;password=Pswd!7312";
        private DatabaseConnection()
        {
            database = new Database(connectionString, DatabaseType.MySQL, MySql.Data.MySqlClient.MySqlClientFactory.Instance);
        }

        public static IDatabase Instance => instance.database;
    }
}
