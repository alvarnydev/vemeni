using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{

    // Class that manages how select queries to the 'jobs' table are performed
    public class JobQuery
    {

        // Reference to database
        public AppDb Db { get; }

        // Constructor
        public JobQuery(AppDb db)
        {
            Db = db;
        }

        // SQl Select command: Select one by id
        public async Task<Job> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user`, `type`, `title`, `description`, `location_lon`, `location_lat`, `date`, `status`, `accepted_by` FROM `jobs` WHERE `id` = @id";
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
        public async Task<List<Job>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `user`, `type`, `title`, `description`, `location_lon`, `location_lat`, `date`, `status`, `accepted_by` FROM `jobs` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        // SQl Delete command: Delete all
        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `jobs`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        // Database reader method to retrieve properties
        private async Task<List<Job>> ReadAllAsync(DbDataReader reader)
        {
            var jobs = new List<Job>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var job = new Job(Db)
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetInt32(1),
                        Type = reader.GetInt32(2),
                        Title = reader.GetString(3),
                        Description = reader.GetString(4),
                        Location_lon = reader.GetDouble(5),
                        Location_lat = reader.GetDouble(6),
                        Date = reader.GetDateTime(7),
                        Status = reader.GetInt32(8),
                        Accepted_by = reader.IsDBNull(9) ? 0 : reader.GetInt32(9)
                    };
                    jobs.Add(job);
                }
            }
            return jobs;
        }
    }
}
