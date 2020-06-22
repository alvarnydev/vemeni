using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Vemini.Models
{
    public class Errand
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user")]
        public int User { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; } 

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location_lon")]
        public double Location_lon { get; set; }

        [JsonProperty("location_lat")]
        public double location_lat { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("accepted_by")]
        public int Accepted_by { get; set; }
    }
}
