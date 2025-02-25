using PetControlSystem.Domain.Entities;
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
        [Range(1, 3, ErrorMessage = "The field {0} must be between {1} and {2}")]
        public int Type { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, 2, ErrorMessage = "The field {0} must be between {1} and {2}")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public CustomerDto Customer { get; set; }
    }
}