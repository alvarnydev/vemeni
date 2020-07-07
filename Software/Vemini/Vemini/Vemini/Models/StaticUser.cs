using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Vemini.Models
{
    /*
     *  Author: Benedikt Blank
     *  Implemented: 06.07.20
     *  Class for static User, that can be accessed from all over the program. 
     */
    static class StaticUser
    {
        [JsonProperty("id")]
        public static int Id { get; set; }

        [JsonProperty("username")]
        public static string UserName { get; set; }

        [JsonProperty("password")]
        public static string Password { get; set; }

        [JsonProperty("firstname")]
        public static string FirstName { get; set; }

        [JsonProperty("lastname")]
        public static string LastName { get; set; }

        [JsonProperty("phone")]
        public static string Phone { get; set; }

        [JsonProperty("email")]
        public static string Email { get; set; }

        [JsonProperty("address_plz")]
        public static string PLZ { get; set; }

        [JsonProperty("address_city")]
        public static string City { get; set; }

        [JsonProperty("address_street")]
        public static string Street { get; set; }

        [JsonProperty("address_strnmbr")]
        public static string StreetNumber { get; set; }

        [JsonProperty("img")]
        public static string Img { get; set; }

        [JsonProperty("lastvisit")]
        public static string LastVisit { get; set; }

        [JsonProperty("created")]
        public static string Created { get; set; }

        [JsonProperty("token")]
        public static string Token { get; set; }

        // Converts a non-static object (user) to a static user
        public static void ToStatic(User user)
        {
            StaticUser.Id = user.Id;
            StaticUser.UserName = user.UserName;
            StaticUser.Password = user.Password;
            StaticUser.FirstName = user.FirstName;
            StaticUser.LastName = user.LastName;
            StaticUser.Phone = user.Phone;
            StaticUser.Email = user.Email;
            StaticUser.PLZ = user.PLZ;
            StaticUser.City = user.City;
            StaticUser.Street = user.Street;
            StaticUser.StreetNumber = user.StreetNumber;
            StaticUser.Img = user.Img;
            StaticUser.LastVisit = user.LastVisit;
            StaticUser.Created = user.Created;
            //Token 
        }
        // Converts the static global user to a non-static user (object)
        public static User ToNonStatic()
        {
            User user = new User();

            user.Id = StaticUser.Id; 
            user.UserName = StaticUser.UserName;
            user.Password = StaticUser.Password;
            user.FirstName = StaticUser.FirstName;
            user.LastName = StaticUser.LastName;
            user.Phone = StaticUser.Phone;
            user.Email = StaticUser.Email;
            user.PLZ = StaticUser.PLZ;
            user.City = StaticUser.City;
            user.Street = StaticUser.Street;
            user.StreetNumber = StaticUser.StreetNumber;
            user.Img = StaticUser.Img;
            user.LastVisit = StaticUser.LastVisit;
            user.Created = StaticUser.Created;
            //Token 

            return user;
          
        }
    }
}
