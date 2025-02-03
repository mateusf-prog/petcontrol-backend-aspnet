namespace PetControlSystem.Domain.Entities
{
    public class Address
    {
        public string? Street { get; private set; }
        public string? Number { get; private set; }
        public string? Complement { get; private set; }
        public string? Neighborhood { get; private set; }
        public string? City { get; private set; }
        public string? State { get; private set; }
        public string? PostalCode { get; private set; }
        public Customer? Customer { get; private set; }
        public User? User { get; private set; }
    }
}
