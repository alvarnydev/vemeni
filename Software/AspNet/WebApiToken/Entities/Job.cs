using System;
using System.Collections.Generic;

namespace WebApiToken.Entities
{
    public class Job
    {
        public Job()
        {
        }

        public int Id { get; set; }
        public int User { get; set; }
        public int Type { get; set; }
        public int Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? AddressPlz { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressStreet { get; set; }
        public string? AddressStrnmbr { get; set; }
        public DateTime? Date { get; set; }
        public int Status { get; set; }
        public int? AcceptedBy { get; set; }

    }
}
