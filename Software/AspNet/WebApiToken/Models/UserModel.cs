using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiToken.Models
{
    public class UserModel
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
#nullable enable
        public string? Phone { get; set; }
        public int? AddressPlz { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressStreet { get; set; }
        public int? AddressStrnmbr { get; set; }
        public string? Img { get; set; }
        public DateTime? Lastvisit { get; set; }
#nullable disable
        public DateTime Created { get; set; }

    }
}
