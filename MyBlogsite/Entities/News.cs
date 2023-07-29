using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyBlogsite.Entities
{
    public class News : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
         public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Required]
        public string Content { get; set; }
        [Required]
        public byte[] NewsPicture { get; set; }
        public User User { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        public int NumberOfComments
        {
            get
            {
                return Comments.Count;
            }
        }
        [NotMapped]
        public ICollection<Comment> Comments { get; set; }
        = new List<Comment>();
    }
}
