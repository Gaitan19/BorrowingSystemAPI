using System.ComponentModel.DataAnnotations;

namespace BorrowingSystemAPI.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Role must be between 3 and 20 characters")]
        public string Role { get; set; }
    }
}
