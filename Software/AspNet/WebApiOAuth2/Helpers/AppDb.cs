using System;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2
{

    // Database class that represents connection to database
    public class AppDb : IDisposable
    {

        // Connection string
        public MySqlConnection Connection { get; }

        // Constructor
        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        // Close connection on dispose
        public void Dispose() => Connection.Dispose();
    }
}
