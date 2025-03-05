namespace PetControlSystem.Domain.Entities
{
    public class Appointment : Entity
    {
        public DateTime Date { get; private set; }
        public string? Description { get; private set; }

        /* EF Relations */
        public Guid CustomerId { get; private set; }
        public Customer? Customer { get; private set; }
        public List<PetSupport> PetSupports { get; private set; }

        public Appointment() { }

        public Appointment(DateTime date, string? description, Guid customerId, List<PetSupport> services)
        {
            Date = date;
            Description = description;
            CustomerId = customerId;
            PetSupports = services;
        }

        public void Update(DateTime date, string? description, Guid customerId, List<PetSupport> services)
        {
            Date = date;
            Description = description;
            CustomerId = customerId;
            PetSupports = services;
        }
    }
}
