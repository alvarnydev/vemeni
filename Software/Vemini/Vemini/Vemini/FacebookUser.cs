using System;
using System.Collections.Generic;
using System.Text;

/*
 * Author: Benedikt Blank with the help of https://github.com/CuriousDrive/PublicProjects/tree/master/OAuthNativeFlow
 * Implemented: 04.06.20
 * Userdata from Facebook Api 
 *
 */
namespace Vemini
{
    public class FacebookEmail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public Picture Picture { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public string Height { get; set; }
        public string Is_Silhouette { get; set; }
        public string Url { get; set; }
        public string Width { get; set; }
    }

}
