using System;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    // Job class that represents 'jobs' table
    public class Job
    {

        // Properties to reflect database entries
        public int Id { get; set; }
        public int User { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Location_lon { get; set; }
        public double Location_lat { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int Accepted_by { get; set; }

        // Reference to database
        internal AppDb Db { get; set; }

        // Constructor
        public Job()
        {
        }

        // Overloaded constructor
        internal Job(AppDb db)
        {
            Db = db;
        }

        // SQL Insert command
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `jobs` (`user`, `type`, `title`, `description`, `location_lon`, `location_lat`, `date`, `status`, `accepted_by`) VALUES (@user, @type, @title, @description, @location_lon, @location_lat, @date, @status, @accepted_by);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        // SQL Update command
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `jobs` SET `user` = @user, `type` = @type, `title` = @title, `description` = @description, `location_lon` = @location_lon, `location_lat` = @location_lat, `date` = @date, `status` = @status, `accepted_by` = @accepted_by WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        // SQL Delete command
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `jobs` WHERE `id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        // Bind id to correctly reference primary key
        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        // Bind parameter to correctly reference sql parameter
        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user",
                DbType = DbType.Int32,
                Value = User,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@type",
                DbType = DbType.Int32,
                Value = Type,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@title",
                DbType = DbType.String,
                Value = Title,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@description",
                DbType = DbType.String,
                Value = Description,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@location_lon",
                DbType = DbType.Double,
                Value = Location_lon,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@location_lat",
                DbType = DbType.Double,
                Value = Location_lat,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@date",
                DbType = DbType.DateTime,
                Value = Date,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@status",
                DbType = DbType.Int32,
                Value = Status,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@accepted_by",
                DbType = DbType.Int32,
                Value = Accepted_by,
            });
        }
    }
}
