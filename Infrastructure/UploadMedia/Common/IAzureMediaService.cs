using Microsoft.AspNetCore.Http;

namespace Infrastructure.UploadMedia.Common
{
    public interface IAzureMediaService
    {
        Task<string> GetMediaUrlAsync(IFormFile file, string userId);
    }
}
