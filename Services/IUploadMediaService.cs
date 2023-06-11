namespace Postr.Services
{
    public interface IUploadMediaService
    {
        Task<string> GetUploadMediaPathAsync(IFormFile file, string userId);
    }
}
