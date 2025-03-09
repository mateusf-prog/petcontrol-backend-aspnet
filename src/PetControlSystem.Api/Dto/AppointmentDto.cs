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

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal? TotalPrice { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public List<AppointmentPetSupportDto> PetSupports { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid PetId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid CustomerId { get; set; }
    }
}
