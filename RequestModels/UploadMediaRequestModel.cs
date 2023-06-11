using System.ComponentModel.DataAnnotations;

namespace Postr.RequestModels
{
    public class UploadMediaRequestModel
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
