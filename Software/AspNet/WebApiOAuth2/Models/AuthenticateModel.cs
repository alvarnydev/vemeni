using System.ComponentModel.DataAnnotations;

namespace WebApiOAuth2.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
