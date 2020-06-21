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
    class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }



        [JsonProperty("username")]
        public string UserName { get; set; }


        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }





        [JsonProperty("address_city")]
        public string City { get; set; }

        [JsonProperty("address_plz")]
        public string PLZ { get; set; }

        [JsonProperty("address_street")]
        public string Street { get; set; }

        [JsonProperty("lastvisit")]
        public string LastVisit { get; set; }

        [JsonProperty("created")]
        public string  Created { get; set; }


        public User() { }

        public User(string id, string email, string userName, string link, string picture, string city, string pLZ, string street, string lastVisit, string created)
        {
            Id = id;
            Email = email;
            UserName = userName;
            Link = link;
            Picture = picture;
            City = city;
            PLZ = pLZ;
            Street = street;
            LastVisit = lastVisit;
            Created = created;
        }

        public override string ToString()
        {
            return $"User: id = {Id},  username = {UserName}, email = {Email},\n" +
                $"created = {Created}, lastvisit = {LastVisit}" ;
        }
    }
}
