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

        [JsonProperty("category")]
        public int Category { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("locationLon")]
        public double LocationLon { get; set; }

        [JsonProperty("locationLat")]
        public double LocationLat { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("acceptedBy")]
        public int AcceptedBy { get; set; }
    }
}
