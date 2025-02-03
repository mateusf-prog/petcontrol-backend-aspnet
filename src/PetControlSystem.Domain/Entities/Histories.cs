namespace PetControlSystem.Domain.Entities
{
    public class Histories : Entity
    {
        public List<Appointment>? Appointments { get; private set; }
        public List<Order>? Order { get; private set; }
    }
}
