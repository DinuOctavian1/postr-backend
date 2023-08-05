using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces.UploadMedia
{
    public interface IUploadMediaService
    {
        Task<ErrorOr<string>> GetUploadMediaPathAsync(IFormFile file, string userId);
    }
}
