using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    public class JobQuery
    {
        public AppDb Db { get; }

        public JobQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Job> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user`, `type`, `title`, `description`, `date`, `status`  FROM `jobs` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Job>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user`, `type`, `title`, `description`, `date`, `status` FROM `jobs` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Job`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Job>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Job>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Job(Db)
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetInt32(1),
                        Type = reader.GetInt32(2),
                        Title = reader.GetString(3),
                        Description = reader.GetString(4),
                        Date = reader.GetDateTime(5),
                        Status = reader.GetInt32(6)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
