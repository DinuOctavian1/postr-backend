using Microsoft.EntityFrameworkCore;
using Postr.Models;

namespace Postr.Data
{
    public class PostrDBContext: DbContext
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
