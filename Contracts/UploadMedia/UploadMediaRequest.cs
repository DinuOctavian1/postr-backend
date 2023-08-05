using Microsoft.AspNetCore.Http;

namespace Contracts.UploadMedia
{
    public record UploadMediaRequest(IFormFile Image);
}
