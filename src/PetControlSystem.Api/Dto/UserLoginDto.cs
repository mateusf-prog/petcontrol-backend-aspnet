using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is in an invalid format")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Compare("Password", ErrorMessage = "The passwords do not match")]
        public string? PasswordConfirm { get; set; }
    }
}