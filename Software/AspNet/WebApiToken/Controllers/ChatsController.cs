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
    public class ChatsController : ControllerBase
    {
        private readonly DataContext _context;

        public ChatsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Chats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChat()
        {
            return NotFound();
            // return await _context.Chats.ToListAsync(); TODO: allow for admins
        }

        // GET: api/Chats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(int id)
        {
            var chats = await _context.Chats.FindAsync(id);

            if (chats == null)
            {
                return NotFound();
            }

            return chats;
        }

        // GET: api/Chats/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChatsToUserId(int id)
        {

            var chats = await _context.Chats
                .FromSqlRaw($"SELECT * FROM chats WHERE user1 = {id} OR user2 = {id}")
                .ToListAsync();

            if (chats == null || chats.Count == 0)
            {
                return NotFound();
            }
            return chats;
        }

        // PUT: api/Chats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(int id, Chat chat)
        {
            if (id != chat.Id)
            {
                return BadRequest();
            }

            _context.Entry(chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(id))
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
                message = $"Successfully changed chat with id {chat.Id}"
            });
        }

        // POST: api/Chats
        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChat", new { id = chat.Id }, chat);
        }

        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChat(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = $"Successfully removed chat with id {chat.Id}"
            });
        }

        private bool ChatExists(int id)
        {
            return _context.Chats.Any(e => e.Id == id);
        }
    }
}
