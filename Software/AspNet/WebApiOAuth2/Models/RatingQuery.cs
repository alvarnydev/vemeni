using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{

    // Class that manages how select queries to the 'ratings' table are performed
    public class RatingQuery
    {

        // Reference to database
        public AppDb Db { get; }

        // Constructor
        public RatingQuery(AppDb db)
        {
            Db = db;
        }

        // SQl Select command: Select one by id
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

        // SQl Select command: Select latest 10 entries by id
        public async Task<List<Rating>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user`, `rating`, `description`, `date`, `given_by`, `job_id` FROM `ratings` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        // SQl Delete command: Delete all
        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `ratings`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        // Database reader method to retrieve properties
        private async Task<List<Rating>> ReadAllAsync(DbDataReader reader)
        {
            var ratings = new List<Rating>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var rating = new Rating(Db)
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetInt32(1),
                        RatingValue = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        Date = reader.GetDateTime(4),
                        Given_by = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                        Job_id = reader.GetInt32(6)
                    };
                    ratings.Add(rating);
                }
            }
            return ratings;
        }
    }
}
