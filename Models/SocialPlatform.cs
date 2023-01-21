
using System.ComponentModel.DataAnnotations;

namespace Postr.Models
{
    public class SocialPlatform
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}