using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Postr.Models
{
    public class User: IdentityUser
    {
        [Required]
        public ICollection<Post> Posts { get; set; }
    }
}
