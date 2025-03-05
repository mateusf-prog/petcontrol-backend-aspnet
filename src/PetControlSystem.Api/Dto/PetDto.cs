using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class PetDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(80, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public double? Weight { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid CustomerId { get; set; }
    }
}