namespace PetControlSystem.Domain.Entities
{
    public class AppointmentPetSupport : Entity
    {
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public Guid PetSupportId { get; set; }
        public PetSupport PetSupport { get; set; }

        public decimal Price { get; set; }

        public AppointmentPetSupport() { }

        public AppointmentPetSupport(Guid petSupportId, Guid appointmentId, decimal price)
        {
            PetSupportId = petSupportId;
            AppointmentId = appointmentId;
            Price = price;
        }
    }
}
