using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    public class ChatQuery
    {
        public AppDb Db { get; }

        public ChatQuery(AppDb db)
        {
            Db = db;
        }

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

        public async Task<List<Chat>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user1`, `user2` FROM `chats` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `chats`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Chat>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Chat>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Chat(Db)
                    {
                        Id = reader.GetInt32(0),
                        User1 = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        User2 = reader.IsDBNull(2) ? 0 : reader.GetInt32(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
