using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class OrderProductDto
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Price { get; set; }
    }
}
