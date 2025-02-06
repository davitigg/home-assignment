using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Requests
{
    public class RegisterUserRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "ICNumber must be between 6 and 20 characters.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "ICNumber must contain only numbers.")]
        public string ICNumber { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid mobile number format.")]
        public string Mobile { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}
