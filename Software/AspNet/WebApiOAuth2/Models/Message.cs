using System;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiOAuth2.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int ChatId { get; set; }

        public string Content { get; set; }

        public int Author { get; set; }

        public int Receiver { get; set; }


        internal AppDb Db { get; set; }

        public Message()
        {
        }

        internal Message(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Messages` (`chat_id`, `content`, `author`, `receiver`) VALUES (@chat_id, @content, @author, @receiver);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Messages` SET `chat_id` = @chat_id, `content` = @content, `author` = @author, `receiver` = @receiver WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Messages` WHERE `id` = @id;";
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
                ParameterName = "@chat_id",
                DbType = DbType.Int32,
                Value = ChatId,
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
