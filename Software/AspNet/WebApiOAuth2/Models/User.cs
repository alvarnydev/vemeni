using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{

    // User class that represents 'users' table
    public class User
    {

        // Properties to reflect database entries
        public int Id { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Address_plz { get; set; }
        public string Address_city { get; set; }
        public string Address_street { get; set; }
        public int Address_strnmbr { get; set; }
        public string Img { get; set; }
        public DateTime Lastvisit { get; set; }
        public DateTime Created { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        // Reference to database
        internal AppDb Db { get; set; }

        // Constructor
        public User()
        {
        }

        // Overloaded constructor
        internal User(AppDb db)
        {
            Db = db;
        }

        // SQL Insert command
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `users` (`username`, `phone`, `email`, `address_plz`, `address_city`, `address_street`, `address_strnmbr`, `img`, `lastvisit`, `created`) VALUES (@username, @phone, @email, @address_plz, @address_city, @address_street, @address_strnmbr, @img, @lastvisit, @created);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        // SQL Update command
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `users` SET `username` = @username, `phone` = @phone, `email` = @email, `address_plz` = @address_plz, `address_city` = @address_city, `address_street` = @address_street, `address_strnmbr` = @address_strnmbr, `img` = @img, `lastvisit` = @lastvisit, `created` = @created WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        // SQL Delete command
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `users` WHERE `id` = @id;";
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
                ParameterName = "@username",
                DbType = DbType.String,
                Value = Username,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@phone",
                DbType = DbType.String,
                Value = Phone,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@email",
                DbType = DbType.String,
                Value = Email,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@address_plz",
                DbType = DbType.Int32,
                Value = Address_plz,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@address_city",
                DbType = DbType.String,
                Value = Address_city,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@address_street",
                DbType = DbType.String,
                Value = Address_street,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@address_strnmbr",
                DbType = DbType.Int32,
                Value = Address_strnmbr,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@img",
                DbType = DbType.String,
                Value = Img,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@lastvisit",
                DbType = DbType.DateTime,
                Value = Lastvisit,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@created",
                DbType = DbType.DateTime,
                Value = Created,
            });
        }

    }
}
