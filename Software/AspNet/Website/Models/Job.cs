using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Website.Models
{
    public class Job
    {

        // Id
        [JsonPropertyName("id")]
        public string Id { get; set; }

        // Creator
        [JsonPropertyName("creator")]
        public string Creator { get; set; }

        // Creator rating
        [JsonPropertyName("creator_rating")]
        public int creator_rating { get; set; }

        // Date
        [JsonPropertyName("date")]
        public string date { get; set; }

        // Title
        [JsonPropertyName("title")]
        public string title { get; set; }

        // Description
        [JsonPropertyName("description")]
        public string description { get; set; }


        // Override ToString
        public override string ToString() => JsonSerializer.Serialize<Job>(this);
        
    }
}
