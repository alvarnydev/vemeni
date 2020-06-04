using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiOAuth2.Models;

namespace WebApiOAuth2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        public AppDb Db { get; }

        public RatingsController(AppDb db)
        {
            Db = db;
        }

        // GET api/ratings
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new RatingQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/ratings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new RatingQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/ratings
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rating body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/ratings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Rating body)
        {
            await Db.Connection.OpenAsync();
            var query = new RatingQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.User = body.User;
            result.RatingValue = body.RatingValue;
            result.Description = body.Description;
            result.Date = body.Date;
            result.Given_By = body.Given_By;
            result.Job_Id = body.Job_Id;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new RatingQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/ratings
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
