using System;
using System.Collections.Generic;

namespace WebApiToken.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int? GivenBy { get; set; }
        public int JobId { get; set; }

    }
}
