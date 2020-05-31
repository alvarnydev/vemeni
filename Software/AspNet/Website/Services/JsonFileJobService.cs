using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Website.Models;
using Microsoft.AspNetCore.Hosting;

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
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "jobs.json"); }
        }

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
    }
}
