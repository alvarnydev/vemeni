using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiToken.Entities;
using WebApiToken.Helpers;

namespace WebApiToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly DataContext _context;

        public RatingsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            return NotFound();
            // return await _context.Ratings.ToListAsync(); TODO: allow for admins
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRatings(int id)
        {
            var ratings = await _context.Ratings.FindAsync(id);

            if (ratings == null)
            {
                return NotFound();
            }

            return ratings;
        }

        // GET: api/Ratings/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatingsToUserId(int id)
        {

            var ratings = await _context.Ratings
                .FromSqlRaw($"SELECT * FROM ratings WHERE user = {id}")
                .ToListAsync();

            if (ratings == null || ratings.Count == 0)
            {
                return NotFound();
            }
            return ratings;
        }

        // PUT: api/Ratings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRatings(int id, Rating ratings)
        {
            if (id != ratings.Id)
            {
                return BadRequest();
            }

            _context.Entry(ratings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ratings
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRatings(Rating ratings)
        {
            _context.Ratings.Add(ratings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRatings", new { id = ratings.Id }, ratings);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rating>> DeleteRatings(int id)
        {
            var ratings = await _context.Ratings.FindAsync(id);
            if (ratings == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(ratings);
            await _context.SaveChangesAsync();

            return ratings;
        }

        private bool RatingsExists(int id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }
    }
}
