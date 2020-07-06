using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

/*
 * Author: Benedikt Blank with the help of https://github.com/CuriousDrive/PublicProjects/tree/master/OAuthNativeFlow
 * Author: Dmitry
 * Implemented: 04.06.20
 * Userdata from Google Api 
 *
 */

namespace Vemini
{
    class UserRefactored
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }


        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
      
        [JsonProperty("img")]
        public string Picture { get; set; }

        [JsonProperty("address_city")]
        public string City { get; set; }

        [JsonProperty("address_plz")]
        public int PLZ { get; set; }

        [JsonProperty("address_street")]
        public string Street { get; set; }

        [JsonProperty("lastvisit")]
        public string LastVisit { get; set; }

        [JsonProperty("created")]
        public string  Created { get; set; }

        [JsonProperty("address_strnmbr")]
        public int Strnmbr { get; set; }

        public UserRefactored() { }


        public UserRefactored(int id, string email, string userName, string phone, string picture, string city, int pLZ, string street, int strnmbr, string lastVisit, string created)
        {
            Id = id;
            Email = email;
            UserName = userName;
            Picture = picture;
            City = city;
            Phone = phone;
            PLZ = pLZ;
            Street = street;
            LastVisit = lastVisit;
            Created = created;
            Strnmbr = strnmbr;
        }

        public override string ToString()
        {
            return $"User: id = {Id},  username = {UserName}, email = {Email},\n" +
                $"created = {Created}, lastvisit = {LastVisit}" ;
        }
    }
}
