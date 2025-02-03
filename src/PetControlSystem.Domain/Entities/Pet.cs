namespace PetControlSystem.Domain.Entities
{
    public class Pet : Entity
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public double? Weight { get; private set; }
        public Customer? Customer { get; private set; }
        public PetType Type { get; private set; }
        public Gender Gender { get; private set; }
    }
}
