namespace PetControlSystem.Domain.Entities
{
    public class AppointmentPetSupport : Entity
    {
        public Guid AppointmentId { get; private set; }
        public Appointment Appointment { get; private set; }

        public Guid PetSupportId { get; private set; }
        public PetSupport PetSupport { get; private set; }

        public decimal Price { get; private set; }

        public AppointmentPetSupport() { }

        public AppointmentPetSupport(Guid appointmentId, Guid petSupportId, decimal price)
        {
            AppointmentId = appointmentId;
            PetSupportId = petSupportId;
            Price = price;
        }
    }
}
