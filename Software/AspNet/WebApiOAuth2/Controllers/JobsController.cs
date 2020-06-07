using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiOAuth2.Models;

namespace WebApiOAuth2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        public AppDb Db { get; }

        public JobsController(AppDb db)
        {
            Db = db;
        }

        // GET api/job
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new JobQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/job/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new JobQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/job
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Job body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/job/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Job body)
        {
            await Db.Connection.OpenAsync();
            var query = new JobQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.User = body.User;
            result.Type = body.Type;
            result.Title = body.Title;
            result.Description = body.Description;
            result.Location_lon = body.Location_lon;
            result.Location_lat = body.Location_lat;
            result.Date = body.Date;
            result.Status = body.Status;
            result.Accepted_by = body.Accepted_by;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/job/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new JobQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/job
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            /*
            await Db.Connection.OpenAsync();
            var query = new ChatQuery(Db);
            await query.DeleteAllAsync();
            */
            return new OkResult();
        }
    }
}
