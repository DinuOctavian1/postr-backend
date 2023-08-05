using Application.Common.Interfaces.UploadMedia;
using Domain.Common.Errors;
using ErrorOr;
using Infrastructure.UploadMedia.Common;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.UploadMedia
{
    public class UploadMediaService : IUploadMediaService
    {
        private readonly IAzureMediaService _azureMediaService;

        public UploadMediaService(IAzureMediaService azureMediaService)
        {
            _azureMediaService = azureMediaService;
        }

        public async Task<ErrorOr<string>> GetUploadMediaPathAsync(IFormFile file, string userId)
        {
            string url = await _azureMediaService.GetMediaUrlAsync(file, userId);
            if (url is null)
                return Errors.UploadMedia.UploadFailed;

            return url;
        }
    }
}
