using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiToken.Models
{
    public class UserPublicModel
    {

        public string Username { get; set; }
        public string Firstname { get; set; }
#nullable enable
        public string? Phone { get; set; }
        public string? Img { get; set; }
#nullable disable

    }
}
