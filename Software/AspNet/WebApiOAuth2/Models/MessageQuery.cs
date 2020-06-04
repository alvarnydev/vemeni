using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    public class MessageQuery
    {
        public AppDb Db { get; }

        public MessageQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Message> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `chat_id`, `content`, `author`, `receiver` FROM `messages` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Message>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `chat_id`, `content`, `author`, `receiver` FROM `messages` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `messages`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Message>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Message>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Message(Db)
                    {
                        Id = reader.GetInt32(0),
                        Chat_Id = reader.GetInt32(1),
                        Content = reader.GetString(2),
                        Author = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        Receiver = reader.IsDBNull(4) ? 0 : reader.GetInt32(4)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
