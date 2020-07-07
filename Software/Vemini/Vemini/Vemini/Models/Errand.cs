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
        public string Category { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("address_plz")]
        public string AddressePlz { get; set; }

        [JsonProperty("address_city")]
        public string AdresseCity { get; set; }

        [JsonProperty("address_street")]
        public string AdresseStreet { get; set; }

        [JsonProperty("address_strnmbr")]
        public string AdresseNmbr { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("accepted_by")]
        public int? AcceptedBy { get; set; }
    }
}
