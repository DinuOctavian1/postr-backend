using System.ComponentModel.DataAnnotations;

namespace Postr.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Url { get; set; }

        [Required]
        [MaxLength(50)]
        public string Alt { get; set; }

        [Required]
        public ICollection<Post> Posts { get; set; }
    }
}