using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string? Name { get; private set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is in an invalid format")]
        public string? Email { get; private set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [PasswordPropertyText]
        public string? Password { get; private set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Phone(ErrorMessage = "The field {0} is in an invalid format")]
        public string? Phone { get; private set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(14, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 11)]
        public string? Document { get; private set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, 2, ErrorMessage = "The field {0} must be between {1} and {2}")]
        public int DocumentType { get; private set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, 2, ErrorMessage = "The field {0} must be between {1} and {2}")]
        public int Type { get; private set; }
    }
}
