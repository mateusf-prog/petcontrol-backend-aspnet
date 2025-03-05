using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is in an invalid format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Phone(ErrorMessage = "The field {0} is in an invalid format")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(14, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 11)]
        public string? Document { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public AddressDto? AddressDto { get; set; }
    }
}
