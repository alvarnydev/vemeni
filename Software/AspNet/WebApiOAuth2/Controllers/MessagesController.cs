using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiOAuth2.Models;

namespace WebApiOAuth2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        public AppDb Db { get; }

        public MessagesController(AppDb db)
        {
            Db = db;
        }

        // GET api/messages
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new MessageQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/messages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new MessageQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/messages
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Message body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/messages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Message body)
        {
            await Db.Connection.OpenAsync();
            var query = new MessageQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.ChatId = body.ChatId;
            result.Content = body.Content;
            result.Author = body.Author;
            result.Receiver = body.Receiver;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new MessageQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/messages
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new MessageQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }
    }
}
