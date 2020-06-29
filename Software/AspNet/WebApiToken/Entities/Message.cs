using System;
using System.Collections.Generic;

namespace WebApiToken.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; }
        public int? Author { get; set; }
        public int? Receiver { get; set; }

    }
}
