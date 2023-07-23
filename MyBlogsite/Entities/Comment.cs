using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyBlogsite.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string CommentString { get; set; }

        public User User { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public News News { get; set; }
        [ForeignKey("NewsId")]
        public int NewsId { get; set; }

    }
}
