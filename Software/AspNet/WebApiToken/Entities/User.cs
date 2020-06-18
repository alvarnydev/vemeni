using System;

namespace WebApiToken.Entities
{
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int AddressPlz { get; set; }
        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public int AddressStrnmbr { get; set; }
        public string Img { get; set; }
        public DateTime Lastvisit { get; set; }
        public DateTime Created { get; set; }

    }
}
