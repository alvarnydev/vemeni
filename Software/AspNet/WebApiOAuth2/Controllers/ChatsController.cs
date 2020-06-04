using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiOAuth2.Models;

namespace WebApiOAuth2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        public AppDb Db { get; }

        public ChatsController(AppDb db)
        {
            Db = db;
        }

        // GET api/chats
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new ChatQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/chats/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ChatQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/chats
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Chat body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/chats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Chat body)
        {
            await Db.Connection.OpenAsync();
            var query = new ChatQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.User1 = body.User1;
            result.User2 = body.User2;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/chats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ChatQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/chats
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new ChatQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }
    }
}
