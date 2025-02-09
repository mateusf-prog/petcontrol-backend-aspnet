namespace PetControlSystem.Domain.Entities
{
    public class Appointment : Entity
    {
        public DateTime Date { get; private set; }
        public string? Description { get; private set; }

        /* EF Relations */
        public Guid CustomerId { get; private set; }
        public Customer? Customer { get; private set; }
        public List<PetSupport>? PetSupports { get; private set; }
    }
}
