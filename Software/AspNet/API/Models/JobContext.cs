using Microsoft.EntityFrameworkCore;


namespace API.Models
{
    public class JobContext : DbContext
    {

        // Constructor
        public JobContext(DbContextOptions<JobContext> options) : base(options)
        {

        }

        // Member variable to store all jobs
        public DbSet<Job> Jobs { get; set; }

    }
}
