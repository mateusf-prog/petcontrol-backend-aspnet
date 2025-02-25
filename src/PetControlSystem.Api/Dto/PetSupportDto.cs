using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class PetSupportDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 5)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0, 9999, ErrorMessage = "The field {0} must be between {1} and {2}")]
        public decimal? SmallDogPrice { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0, 9999, ErrorMessage = "The field {0} must be between {1} and {2}")]
        public decimal? MediumDogPrice { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0, 9999, ErrorMessage = "The field {0} must be between {1} and {2}")]
        public decimal? LargeDogPrice { get; set; }
    }
}
