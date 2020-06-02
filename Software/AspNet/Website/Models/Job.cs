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
        public int Id { get; set; }

        // Image
        [JsonPropertyName("image")]
        public string Image { get; set; }

        // Creator
        [JsonPropertyName("creator")]
        public string Creator { get; set; }

        // Creator rating
        [JsonPropertyName("creator_ratings")]
        public int[] Creator_ratings { get; set; }

        // Date
        [JsonPropertyName("date")]
        public string Date { get; set; }

        // Title
        [JsonPropertyName("title")]
        public string Title { get; set; }

        // Description
        [JsonPropertyName("description")]
        public string Description { get; set; }


        // Override ToString
        public override string ToString() => JsonSerializer.Serialize<Job>(this);
        
    }
}
