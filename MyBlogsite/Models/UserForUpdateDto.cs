using System.ComponentModel.DataAnnotations;

namespace MyBlogsite.Models
{
    public class UserForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(20)]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        public byte[] ProfilePicture { get; set; } = null;
    }
}
