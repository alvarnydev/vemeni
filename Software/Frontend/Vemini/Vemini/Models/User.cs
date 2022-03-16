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
        public int Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("address_plz")]
        public string PLZ { get; set; }

        [JsonProperty("address_city")]
        public string City { get; set; }

        [JsonProperty("address_street")]
        public string Street { get; set; }

        [JsonProperty("address_strnmbr")]
        public string StreetNumber { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("lastvisit")]
        public string LastVisit { get; set; }

        [JsonProperty("created")]
        public string  Created { get; set; }


        public User() { }

        public User(int id, string userName, string email, string link, string picture, string city, string pLZ, string street, string lastVisit, string created)
        {
            Id = id;
            Email = email;
            UserName = userName;
            Img = picture;
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
