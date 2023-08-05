using Azure.Storage.Blobs;
using Infrastructure.UploadMedia.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.UploadMedia.Azure
{
    public class AzureMediaService : IAzureMediaService
    {
        private readonly AzureSettings _azureSettings;

        public AzureMediaService(IOptions<AzureSettings> azureSettings)
        {
            _azureSettings = azureSettings.Value;
        }

        public async Task<string> GetMediaUrlAsync(IFormFile file, string userId)
        {
            string fileName = $"{userId}{Path.GetFileName(file.FileName)}";
            string containerName = _azureSettings.BlobContainer;
            string connectionString = _azureSettings.Connection;
            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(fileName);


            if (blob == null)
                return null;

            if (await blob.ExistsAsync())
                //TODO :Perform content check
                return blob.Uri.ToString();

            Stream stream = file.OpenReadStream();
            await blob.UploadAsync(stream);

            return blob.Uri.ToString();

        }
    }
}
