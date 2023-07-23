using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyBlogsite.Entities
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public byte[] NewsPicture { get; set; }
        public User User { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public int NumberOfComments
        {
            get
            {
                return Comments.Count;
            }
        }
        [InverseProperty("News")]
        public ICollection<Comment> Comments { get; set; }
        = new List<Comment>();
    }
}
