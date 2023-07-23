using MyBlogsite.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyBlogsite.Models
{
    public class NewsForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public byte[] NewsPicture { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int NumberOfComments
        {
            get
            {
                return Comments.Count;
            }
        }
        public ICollection<Comment> Comments { get; set; }
        = new List<Comment>();
    }
}
