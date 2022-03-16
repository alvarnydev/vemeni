using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Vemini.Models
{
    class Chat
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user1")]
        public string User1 { get; set; }

        [JsonProperty("user2")]
        public string User2 { get; set; }

    }
}
