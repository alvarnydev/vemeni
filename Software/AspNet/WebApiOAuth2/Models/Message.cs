using System;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    // Message class that represents 'messages' table
    public class Message
    {

        // Properties to reflect database entries
        public int Id { get; set; }
        public int Chat_Id { get; set; }
        public string Content { get; set; }
        public int Author { get; set; }
        public int Receiver { get; set; }

        // Reference to database
        internal AppDb Db { get; set; }

        // Constructor
        public Message()
        {
        }

        // Overloaded constructor
        internal Message(AppDb db)
        {
            Db = db;
        }

        // SQL Insert command
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Messages` (`chat_id`, `content`, `author`, `receiver`) VALUES (@chat_id, @content, @author, @receiver);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        // SQL Update command
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Messages` SET `chat_id` = @chat_id, `content` = @content, `author` = @author, `receiver` = @receiver WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        // SQL Delete command
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Messages` WHERE `id` = @id;";
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
                ParameterName = "@chat_id",
                DbType = DbType.Int32,
                Value = Chat_Id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@content",
                DbType = DbType.String,
                Value = Content,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@author",
                DbType = DbType.Int32,
                Value = Author,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@receiver",
                DbType = DbType.Int32,
                Value = Receiver,
            });
        }

    }
}
