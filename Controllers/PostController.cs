using Microsoft.AspNetCore.Mvc;
using Postr.RequestModels;
using Postr.Services;

namespace Postr.Controllers
{
    public class PostController : BaseApiController
    {
        private readonly IPostGeneratorService _postGeneratorService;
        private readonly IUploadMediaService _uploadMediaService;

        public PostController(IPostGeneratorService postGeneratorService, IUploadMediaService uploadMediaService)
        {
            _postGeneratorService = postGeneratorService;
            _uploadMediaService = uploadMediaService;

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

        
        [HttpPost("generate")]
        public async Task<IActionResult> PostAsync([FromBody] PostRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _postGeneratorService.GeneratePostAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Something went wrong");
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromForm] UploadMediaRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var videoPath = await _uploadMediaService.GetUploadMediaPathAsync(model.Image);

                if (videoPath is null)
                {
                    return BadRequest("The quiz you have uploaded is not valid");
                }

                return Ok(videoPath);
            }

            return BadRequest("The file is corrupt");
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
