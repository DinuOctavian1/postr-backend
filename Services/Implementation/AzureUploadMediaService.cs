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

        public async Task<string> GetUploadMediaPathAsync(IFormFile file)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string containerName = _configuration.GetSection("AzureSettings").GetValue<string>("BlobContainer");
            string connectionString = _configuration.GetConnectionString("AzureBlob");
            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(fileName);
            Stream stream = file.OpenReadStream();
            await blob.UploadAsync(stream);

            return blob.Uri.ToString();
        }
    }
}
