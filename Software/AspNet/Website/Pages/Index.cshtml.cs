using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Website.Models;
using Website.Services;

namespace Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileJobService JobService; // Variable to "store" retrieval service
        public IEnumerable<Job> Jobs { get; private set; } // Job "list"

        public IndexModel(
            ILogger<IndexModel> logger,
            JsonFileJobService jobService // "Ask" for JsonFileJobService when created
            )
        {
            _logger = logger;
            JobService = jobService;
        }

        // On httml get request
        public void OnGet()
        {
            Jobs = JobService.GetJobs();
        }
    }
}
