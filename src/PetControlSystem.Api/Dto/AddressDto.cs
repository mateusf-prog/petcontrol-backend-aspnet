using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class AddressDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string? Street { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(9999, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
        public string? Number { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 5)]
        public string? Complement { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 4)]
        public string? Neighborhood { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 4)]
        public string? City { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(2, ErrorMessage = "The field {0} must be {1} characters", MinimumLength = 2)]
        public string? State { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(8, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 8)]
        public string? ZipCode { get; set; }

        public string? CustomerName { get; set; }
        public string? UserName { get; set; }
    }
}