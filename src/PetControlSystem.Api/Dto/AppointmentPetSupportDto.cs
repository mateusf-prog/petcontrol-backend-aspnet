namespace PetControlSystem.Api.Dto
{
    public class AppointmentPetSupportDto
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid PetSupportId { get; set; }
        public decimal Price { get; set; }
    }
}