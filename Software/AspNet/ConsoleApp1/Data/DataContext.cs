using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Models;
namespace ConsoleApp1.Data
{
    public class DataContext : DbContext
    {

        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;Database=test2;user id=vmnuser;password=B77yDbffmKe6");
        }
    }
}
