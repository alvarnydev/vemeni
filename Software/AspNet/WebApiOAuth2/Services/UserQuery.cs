using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WebApiOAuth2.Models;

namespace WebApiOAuth2.Services
{

    // Class that manages how select queries to the 'users' table are performed
    public class UserQuery
    {

        // Reference to database
        public AppDb Db { get; }

        // Constructor
        public UserQuery(AppDb db)
        {
            Db = db;
        }

        // SQl Select command: Select one by id
        public async Task<User> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `username`, `phone`, `email`, `address_plz`, `address_city`, `address_street`, `address_strnmbr`, `img`, `lastvisit`, `created` FROM `users` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        // SQl Select command: Select latest 10 entries by id
        public async Task<List<User>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `username`, `phone`, `email`, `address_plz`, `address_city`, `address_street`, `address_strnmbr`, `img`, `lastvisit`, `created` FROM `users` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        // SQl Delete command: Delete all
        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `users`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        // Database reader method to retrieve properties
        private async Task<List<User>> ReadAllAsync(DbDataReader reader)
        {
            var users = new List<User>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new User(Db)
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Email = reader.GetString(3),
                        Address_plz = reader.GetInt32(4),
                        Address_city = reader.GetString(5),
                        Address_street = reader.GetString(6),
                        Address_strnmbr = reader.GetInt32(7),
                        Img = reader.GetString(8),
                        Lastvisit = reader.GetDateTime(9),
                        Created = reader.GetDateTime(10)
                    };
                    users.Add(user);
                }
            }
            return users;
        }
    }
}
