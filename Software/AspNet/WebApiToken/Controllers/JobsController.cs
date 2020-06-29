using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiToken.Entities;
using WebApiToken.Helpers;

namespace WebApiToken.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly DataContext _context;

        public JobsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return NotFound();
            //return await _context.Jobs.ToListAsync(); TODO: allow for admins
        }

        // GET: api/Jobs/city/berlin
        [HttpGet("city/{city}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsInCity(string city)
        {

            var jobs = await _context.Jobs
                //.FromSqlRaw($"SELECT * FROM Jobs j INNER JOIN Users u ON j.user = u.id WHERE u.address_city = \"{cityUpper}\"")
                //.FromSqlRaw("SELECT * FROM Jobs j INNER JOIN Users u ON j.user = u.id WHERE u.address_city = \"Berlin\"")
                .FromSqlRaw($"SELECT j.* FROM jobs j, users u WHERE j.user = u.id AND u.address_city = \"{city}\" AND j.status = 0")
                .ToListAsync();

            if (jobs == null || jobs.Count == 0)
            {
                return NotFound();
            }
            return jobs;
        }

        // GET: api/Jobs/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsToUserId(int id)
        {

            var jobs = await _context.Jobs
                .FromSqlRaw($"SELECT * FROM jobs WHERE user = {id}")
                .ToListAsync();

            if (jobs == null || jobs.Count == 0)
            {
                return NotFound();
            }
            return jobs;
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJobs(int id)
        {
            var jobs = await _context.Jobs.FindAsync(id);

            if (jobs == null)
            {
                return NotFound();
            }

            return jobs;
        }

        // PUT: api/Jobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobs(int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new
            {
                message = $"Successfully changed job with id {job.Id}"
            });
        }

        // POST: api/Jobs
        [HttpPost]
        public async Task<ActionResult<Job>> PostJobs(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobs", new { id = job.Id }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJobs(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = $"Successfully removed job with id {job.Id}"
            });
        }

        private bool JobsExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
