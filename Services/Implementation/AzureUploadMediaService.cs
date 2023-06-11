using Azure;
using Azure.Storage.Blobs;

namespace Postr.Services.Implementation
{
    public class AzureUploadMediaService : IUploadMediaService
    {
        private readonly IConfiguration _configuration;
        

        public AzureUploadMediaService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetUploadMediaPathAsync(IFormFile file, string userId)
        {
           // string fileName = $"{userId}{Path.GetExtension(file.FileName)}";
            string fileName = $"{userId}{Path.GetFileName(file.FileName)}";
            string containerName = _configuration.GetSection("AzureSettings").GetValue<string>("BlobContainer");
            string connectionString = _configuration.GetConnectionString("AzureBlob");
            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(fileName);

            
            if (blob == null)
            {
                return null;
            }

            if (await blob.ExistsAsync())
            {
                //TODO :Perform content check
                return blob.Uri.ToString();
            }

            Stream stream = file.OpenReadStream();
            await blob.UploadAsync(stream);

            return blob.Uri.ToString();

        }

        
    }
}
