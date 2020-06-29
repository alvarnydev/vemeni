using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Website.Models;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices.ComTypes;
using System.CodeDom.Compiler;

namespace Website.Services
{
    public class JsonFileJobService
    {

        // Constructor, retrieve webhostenvironment service
        public JsonFileJobService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        // Getter for WebHostEnvironment (knows where things are located)
        public IWebHostEnvironment WebHostEnvironment { get; }

        // Method to retrieve path to json file
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "jobs.json");

        // Returns json jobs as ienumerable (grandfather of lists, can now be used as list, array, collection or something else)
        public IEnumerable<Job> GetJobs()
        {

            // Using Json File Reader to read json file
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {

                // Deserialize json file, make job classes out of them
                return JsonSerializer.Deserialize<Job[]>(jsonFileReader.ReadToEnd(),

                    // Options
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        // Add rating
        public void AddRating(string creatorName, int rating)
        {
            IEnumerable<Job> jobs = GetJobs();

            // LINQ 
            var query = jobs.First(x => x.Creator == creatorName);

            // If there are no ratings yet
            if (query.Creator_ratings == null)
            {
                // Create list
                query.Creator_ratings = new int[] { rating };
            }
            // If ratings already exist
            else
            {
                // Add to list
                var ratingList = query.Creator_ratings.ToList();
                ratingList.Add(rating);
                query.Creator_ratings = ratingList.ToArray();
            }

            // Open json file
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Job>>(

                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    jobs

                );
            }

        }
    }
}
