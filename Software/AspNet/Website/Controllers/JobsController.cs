using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Services;

namespace Website.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {

        // Store JsonFileJobService here
        public JsonFileJobService JobService { get; }

        // "Ask" for JsonFileJobService when being constructed
        public JobsController(JsonFileJobService jobService)
        {
            this.JobService = jobService;
        }

        // Retrieve jobs
        [HttpGet]
        public IEnumerable<Job> Get()
        {
            return JobService.GetJobs();
        }

        // Add rating
        [Route("Rate")]
        [HttpGet] // HttpPatch
        public ActionResult Get(
            [FromQuery] string creatorName, // [FromBody]
            [FromQuery] int Rating // [FromBody]
            )
        {
            JobService.AddRating(creatorName, Rating);
            return Ok();
        }

    }
}
