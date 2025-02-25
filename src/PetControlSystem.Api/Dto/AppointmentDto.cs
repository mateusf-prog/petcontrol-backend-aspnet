using System.ComponentModel.DataAnnotations;

namespace PetControlSystem.Api.Dto
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 5)]
        public string? Description { get; set; }

        public PetDto Pet { get; set; }
        public CustomerDto Customer { get; set; }
        public AddressDto Address { get; set; }
    }
}
