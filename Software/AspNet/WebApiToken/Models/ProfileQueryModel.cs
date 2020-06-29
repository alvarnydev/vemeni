using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiToken.Models
{
    public class ProfileQueryModel
    {
        [Required]
        public int Id { get; set; }
    }
}
