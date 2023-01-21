
using System.ComponentModel.DataAnnotations;

namespace Postr.Models
{
    public class SocialPlatform
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public ICollection<Post> Posts { get; set; }
    }
}