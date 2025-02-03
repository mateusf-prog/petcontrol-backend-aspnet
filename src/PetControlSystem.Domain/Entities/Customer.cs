
namespace PetControlSystem.Domain.Entities
{
    public class Customer : Entity
    {
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Phone { get; private set; }
        public string? Document { get; private set; }
        public List<Order>? Orders { get; private set; }
        public List<Appointment>? Appointments { get; private set; }
    }

    public class Pet : Entity
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public Customer? Customer { get; private set; }
        public PetType Type { get; private set; }
        public Gender Gender { get; private set; }
    }
}
