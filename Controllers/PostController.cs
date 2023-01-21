using Microsoft.AspNetCore.Mvc;
using Postr.Models;
using Postr.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Postr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostGeneratorService _postGeneratorService;

        public PostController(IPostGeneratorService postGeneratorService)
        {
            _postGeneratorService = postGeneratorService;
        }

        // GET: api/<PostController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _postGeneratorService.GeneratePost(model.Input);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Something went wrong");
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
