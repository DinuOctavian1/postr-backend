using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Postr.Models;

namespace Postr.Data
{
    public class PostrDBContext: IdentityDbContext<IdentityUser>
    {
        public PostrDBContext(DbContextOptions<PostrDBContext> options) : base(options)
        {

        }


        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get;set; }
        public DbSet<SocialPlatform> SocialPlatforms { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
