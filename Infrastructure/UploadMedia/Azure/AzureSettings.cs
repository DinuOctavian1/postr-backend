namespace Infrastructure.UploadMedia.Azure
{
    public class AzureSettings
    {
        public const string SectionName = "AzureSettings";
        public string BlobContainer { get; set; }
        public string Connection { get; set; }
    }
}
