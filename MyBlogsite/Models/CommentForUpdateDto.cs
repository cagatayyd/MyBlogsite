using MyBlogsite.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyBlogsite.Models
{
    public class CommentForUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string CommentString { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public News news { get; set; }
    }
}
