using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// TODO: use virtual navigation properties to combat SQL injection
// TODO: TOKENS!


namespace WebApiToken
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

// Create database while building application: Add-Migration InitialCreate --> Update-Database
// Reverse-Engineer from existing database: Scaffold-DbContext "server=127.0.0.1;port=3306;Database=vergissmeinnicht;user id=vmnuser;password=B77yDbffmKe6" MySql.Data.EntityFrameworkCore -OutputDir Models -Context DataContext -DataAnnotations