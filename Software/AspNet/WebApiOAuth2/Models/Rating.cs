using System;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public int User { get; set; }

        public int RatingValue { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Given_By { get; set; }

        public int Job_Id { get; set; }


        internal AppDb Db { get; set; }

        public Rating()
        {
        }

        internal Rating(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `ratings` (`user`, `rating`, `description`, `date`, `given_by`, `job_id`) VALUES (@user, @rating, @description, @date, @given_by, @job_id);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `ratings` SET `user` = @user, `rating` = @rating, `description` = @description , `date` = @date , `given_by` = @given_by , `job_id` = @job_id WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `ratings` WHERE `id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

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
                Value = Given_By,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@job_id",
                DbType = DbType.Int32,
                Value = Job_Id,
            });
        }

    }
}
