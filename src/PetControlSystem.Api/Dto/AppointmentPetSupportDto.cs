using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class AppointmentPetSupportDto
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public Guid PetSupportId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Price { get; set; }
    }
}