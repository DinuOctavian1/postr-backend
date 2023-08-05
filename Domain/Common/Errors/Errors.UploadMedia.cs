using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class UploadMedia
        {
            public static Error UploadFailed = Error.Failure(code: "Upload.UploadMediaFailed",
                                                                  description: "Media upload failed.");
        }
    }
}
