using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    public class UserQuery
    {
        public AppDb Db { get; }

        public UserQuery(AppDb db)
        {
            Db = db;
        }

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

        public async Task<List<User>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `username`, `phone`, `email`, `address_plz`, `address_city`, `address_street`, `address_strnmbr`, `img`, `lastvisit`, `created` FROM `users` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `users`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<User>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<User>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new User(Db)
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Email = reader.GetString(3),
                        Address_Plz = reader.GetInt32(4),
                        Address_City = reader.GetString(5),
                        Address_Street = reader.GetString(6),
                        Address_Strnmbr = reader.GetInt32(7),
                        Img = reader.GetString(8),
                        Lastvisit = reader.GetDateTime(9),
                        Created = reader.GetDateTime(10)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
