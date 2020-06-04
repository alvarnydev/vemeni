using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int AddressPlz { get; set; }

        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public int AddressStrnmbr { get; set; }

        public string Img { get; set; }

        public DateTime Lastvisit { get; set; }

        public DateTime Created { get; set; }


        internal AppDb Db { get; set; }

        public User()
        {
        }

        internal User(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `users` (`username`, `phone`, `email`, `address_plz`, `address_city`, `address_street`, `address_strnmbr`, `img`, `lastvisit`, `created`) VALUES (@username, @phone, @email, @address, @img, @lastvisit, @created);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `users` SET `username` = @username, `phone` = @phone, `email` = @email, `address_plz` = @address_plz, `address_city` = @address_city, `address_street` = @address_street, `address_strnmbr` = @address_strnmbr, `img` = @img, `lastvisit` = @lastvisit, `created` = @created WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `users` WHERE `id` = @id;";
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
                Value = AddressPlz,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@address_city",
                DbType = DbType.String,
                Value = AddressPlz,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@address_street",
                DbType = DbType.String,
                Value = AddressPlz,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@address_strnmbr",
                DbType = DbType.Int32,
                Value = AddressPlz,
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
