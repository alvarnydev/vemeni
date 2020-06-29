using System;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{

    // Rating class that represents 'ratings' table
    public class Rating
    {

        // Properties to reflect database entries
        public int Id { get; set; }
        public int User { get; set; }
        public int RatingValue { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Given_by { get; set; }
        public int Job_id { get; set; }

        // Reference to database
        internal AppDb Db { get; set; }

        // Constructor
        public Rating()
        {
        }

        // Overloaded constructor
        internal Rating(AppDb db)
        {
            Db = db;
        }

        // SQL Insert command
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `ratings` (`user`, `rating`, `description`, `date`, `given_by`, `job_id`) VALUES (@user, @rating, @description, @date, @given_by, @job_id);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        // SQL Update command
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `ratings` SET `user` = @user, `rating` = @rating, `description` = @description , `date` = @date , `given_by` = @given_by , `job_id` = @job_id WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        // SQL Delete command
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `ratings` WHERE `id` = @id;";
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
                ParameterName = "@rating",
                DbType = DbType.Int32,
                Value = RatingValue,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@description",
                DbType = DbType.String,
                Value = Description,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@date",
                DbType = DbType.DateTime,
                Value = Date,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@given_by",
                DbType = DbType.Int32,
                Value = Given_by,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@job_id",
                DbType = DbType.Int32,
                Value = Job_id,
            });
        }

    }
}
