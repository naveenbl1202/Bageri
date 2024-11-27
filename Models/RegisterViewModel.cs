using System.ComponentModel.DataAnnotations;

namespace SkaftoBageriWMS.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
