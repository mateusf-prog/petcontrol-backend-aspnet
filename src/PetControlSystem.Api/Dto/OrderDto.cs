using PetControlSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        [Range(0.01, double.MaxValue , ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public CustomerDto Customer { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public List<ProductDto> Products { get; set; }
    }
}