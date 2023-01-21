using System.ComponentModel.DataAnnotations;

namespace Postr.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public  ICollection<SocialPlatform> SocialPlatform { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}