using System;
using System.Collections.Generic;

namespace WebApiToken.Entities
{
    public class Chat
    {
        public Chat()
        {
        }

        public int Id { get; set; }
        public int? User1 { get; set; }
        public int? User2 { get; set; }

    }
}
