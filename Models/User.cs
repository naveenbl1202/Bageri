using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

namespace SkaftoBageriWMS.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        // New Role field with a default value of "User"
        public string Role { get; set; } = "User";  // Default role set to "User"

        // Hash the password before saving it to the database
        public string PasswordHash => BCrypt.Net.BCrypt.HashPassword(Password);

        // Method to verify the entered password against the stored hash
        public bool VerifyPassword(string enteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, PasswordHash);
        }
    }
}
