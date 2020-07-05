using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Vemini.Models
{
    class PostUserRequest
    {

        [JsonProperty("id")]
        public int Id { get; set; }

    }
}
