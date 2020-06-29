using System;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{

    // Chat class that represents 'chats' table
    public class Chat
    {

        // Properties to reflect database entries
        public int Id { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }

        // Reference to database
        internal AppDb Db { get; set; }

        // Constructor
        public Chat()
        {
        }

        // Overloaded constructor
        internal Chat(AppDb db)
        {
            Db = db;
        }

        // SQL Insert command
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `chats` (`user1`, `user2`) VALUES (@user1, @user2);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        // SQL Update command
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `chats` SET `user1` = @user1, `user2` = @user2 WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        // SQL Delete command
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `chats` WHERE `id` = @id;";
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
                ParameterName = "@user1",
                DbType = DbType.Int32,
                Value = User1,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user2",
                DbType = DbType.Int32,
                Value = User2,
            });
        }

    }
}
