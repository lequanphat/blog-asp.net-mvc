using Microsoft.EntityFrameworkCore;
namespace Blog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
    }
}