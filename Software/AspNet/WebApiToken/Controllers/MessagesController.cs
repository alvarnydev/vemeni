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
    public class MessagesController : ControllerBase
    {
        private readonly DataContext _context;

        public MessagesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return NotFound();
            // return await _context.Messages.ToListAsync(); TODO: allow for admins
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessages(int id)
        {
            var messages = await _context.Messages.FindAsync(id);

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        // GET: api/Messages/chat/5
        [HttpGet("chat/{id}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesToChatId(int id)
        {

            var messages = await _context.Messages
                .FromSqlRaw($"SELECT * FROM messages WHERE chat_id = {id}")
                .ToListAsync();

            if (messages == null || messages.Count == 0)
            {
                return NotFound();
            }
            return messages;
        }

        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessages(int id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessagesExists(id))
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
                message = $"Successfully changed message with id {message.Id}"
            });
        }

        // POST: api/Messages
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessages(Message messages)
        {
            await _context.Messages.AddAsync(messages);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessages", new { id = messages.Id }, messages);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessages(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = $"Successfully removed messages with id {message.Id}"
            });
        }

        private bool MessagesExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
