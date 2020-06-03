using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;

namespace WebApOAuth2.Models
{
    public class Job
    {
        public long JobId { get; set; }

        public long CreatorId { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string DateCreated { get; set; }

        public int Status { get; set; }

    }
}
