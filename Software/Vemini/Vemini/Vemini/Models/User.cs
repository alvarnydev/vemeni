using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

/*
 * Author: Benedikt Blank with the help of https://github.com/CuriousDrive/PublicProjects/tree/master/OAuthNativeFlow
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

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; } // TODO DELETE

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; } // TODO DELETE

        [JsonProperty("family_name")]
        public string FamilyName { get; set; } // TODO DELETE

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; } // TODO DELETE




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

        public User(string id, string email, bool verifiedEmail, string name, string givenName, string familyName, string link, string picture, string gender, string city, string pLZ, string street, string lastVisit, string created
            )
        {
            Id = id;
            Email = email;
            VerifiedEmail = verifiedEmail;
            UserName = name;
            GivenName = givenName;
            FamilyName = familyName;
            Link = link;
            Picture = picture;
            Gender = gender;
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
