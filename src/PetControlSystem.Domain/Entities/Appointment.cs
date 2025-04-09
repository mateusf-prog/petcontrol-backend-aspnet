namespace PetControlSystem.Domain.Entities
{
    public class Appointment : Entity
    {
        public DateTime Date { get; private set; }
        public string? Description { get; private set; }
        public decimal? TotalPrice { get; private set; }

        /* EF Relations */
        public Guid CustomerId { get; private set; }
        public Customer? Customer { get; private set; }
        public Guid PetId { get; private set; }
        public Pet? Pet { get; private set; }
        public List<AppointmentPetSupport> AppointmentPetSupports { get; private set; }

        public Appointment() { }

        public Appointment(DateTime date, string? description, decimal? totalPrice, Guid customerId, Guid petId, List<AppointmentPetSupport> petSupports)
        {
            Date = date;
            Description = description;
            TotalPrice = totalPrice;
            CustomerId = customerId;
            PetId = petId;
            AppointmentPetSupports = petSupports;
        }

        public void Update(DateTime date, string? description, decimal? totalPrice, List<AppointmentPetSupport> petSupports)
        {
            Date = date;
            Description = description;
            TotalPrice = totalPrice;
            AppointmentPetSupports = petSupports;
        }
    }
}
