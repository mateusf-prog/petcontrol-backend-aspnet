using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(80, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public int Stock { get; set; }

        public string? Description { get; set; }
    }
}
