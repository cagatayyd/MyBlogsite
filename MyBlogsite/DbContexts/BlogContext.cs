using Microsoft.EntityFrameworkCore;
using MyBlogsite.Entities;
using System.Xml.Linq;

namespace MyBlogsite.DbContexts
{
    public class BlogContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<News> News { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;


        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
