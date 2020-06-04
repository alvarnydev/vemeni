using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    public class RatingQuery
    {
        public AppDb Db { get; }

        public RatingQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Rating> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user`, `rating`, `description`, `date`, `given_by`, `job_id` FROM `ratings` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Rating>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user`, `rating`, `description`, `date`, `given_by`, `job_id` FROM `ratings` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `ratings`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Rating>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Rating>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Rating(Db)
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetInt32(1),
                        RatingValue = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        Date = reader.GetDateTime(4),
                        GivenBy = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                        JobId = reader.GetInt32(6)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
