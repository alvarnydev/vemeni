using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Vemini.Models
{
    public class Errand
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } 

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location_lon")]
        public string Location_lon { get; set; }

        [JsonProperty("location_lat")]
        public string location_lat { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("accepted_by")]
        public string Acceptedd_by { get; set; }
    }
}
