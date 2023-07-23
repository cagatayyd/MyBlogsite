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
            modelBuilder.Entity<Comment>()
                .HasOne(a => a.News)
                .WithMany(b=>b.Comments)
                .HasForeignKey("NewsId")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(a => a.User)
                .WithMany(b => b.Comments)
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
