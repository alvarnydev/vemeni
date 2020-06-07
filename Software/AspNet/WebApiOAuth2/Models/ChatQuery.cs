using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{

    // Class that manages how select queries to the 'chats' table are performed
    public class ChatQuery
    {

        // Reference to database
        public AppDb Db { get; }

        // Constructor
        public ChatQuery(AppDb db)
        {
            Db = db;
        }

        // SQl Select command: Select one by id
        public async Task<Chat> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user1`, `user2` FROM `chats` WHERE `id` = @id";
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
        public async Task<List<Chat>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user1`, `user2` FROM `chats` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        // SQl Delete command: Delete all
        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `chats`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        // Database reader method to retrieve properties
        private async Task<List<Chat>> ReadAllAsync(DbDataReader reader)
        {
            var chats = new List<Chat>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var chat = new Chat(Db)
                    {
                        Id = reader.GetInt32(0),
                        User1 = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        User2 = reader.IsDBNull(2) ? 0 : reader.GetInt32(2)
                    };
                    chats.Add(chat);
                }
            }
            return chats;
        }
    }
}
