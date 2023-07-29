using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogsite.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Surname { get; set; }
        [Required]
        [StringLength(20)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public byte[] ProfilePicture { get; set; } = null;

        [NotMapped]
        public ICollection<Comment> Comments { get; set; }
        = new List<Comment>();

    }
}
