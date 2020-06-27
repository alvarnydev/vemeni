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
        public async Task<IActionResult> PutRatings(int id, Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

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

            return Ok(new
            {
                message = $"Successfully changed rating with id {rating.Id}"
            });
        }

        // POST: api/Ratings
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRatings(Rating ratings)
        {
            await _context.Ratings.AddAsync(ratings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRatings", new { id = ratings.Id }, ratings);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRatings(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = $"Successfully removed rating with id {rating.Id}"
            });
        }

        private bool RatingsExists(int id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }
    }
}
