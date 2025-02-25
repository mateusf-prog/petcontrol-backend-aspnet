using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        [Range(0.01, double.MaxValue , ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal TotalPrice { get; set; }

        public string? CustomerName { get; set; }

        public List<ProductDto>? Products { get; set; }
    }
}