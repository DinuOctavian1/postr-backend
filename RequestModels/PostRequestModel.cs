
using System.ComponentModel.DataAnnotations;

namespace Postr.RequestModels
{
    public class PostRequestModel
    {
        [Required]
        public string SocialMediaPlatform { get; set; }

        [Required]
        public string PostDescription { get; set; }

        [Required]
        public string PageCategories { get; set; }

        [Required]
        public string PageName { get; set; }
    }
}
