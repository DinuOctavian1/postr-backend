using Application.Common.Interfaces.UploadMedia;
using Contracts.UploadMedia;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class UploadController : BaseApiController
    {
        private readonly IUploadMediaService _uploadMediaService;

        public UploadController(IUploadMediaService uploadMediaService)
        {
            _uploadMediaService = uploadMediaService;
        }

        [HttpPost]
        public async Task<ActionResult> UploadMedia([FromForm] UploadMediaRequest request)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ErrorOr<string> uploadResult = await _uploadMediaService.GetUploadMediaPathAsync(request.Image, userId);

            return uploadResult.Match<ActionResult>(
                               uploadResult => Ok(uploadResult),
                               errors => Problem(errors));
        }
    }
}
