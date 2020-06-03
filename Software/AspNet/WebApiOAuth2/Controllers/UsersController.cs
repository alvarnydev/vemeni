using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiOAuth2.Models;

namespace WebApiOAuth2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public AppDb Db { get; }

        public UsersController(AppDb db)
        {
            Db = db;
        }

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] User body)
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Username = body.Username;
            result.Phone = body.Phone;
            result.Email = body.Email;
            result.Address = body.Address;
            result.Img = body.Img;
            result.Lastvisit = body.Lastvisit;
            result.Created = body.Created;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/users
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }
    }
}
