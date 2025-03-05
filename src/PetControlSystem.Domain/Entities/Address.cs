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

        public Address() { }

        public Address(string? street, string? number, string? complement, string? neighborhood, string? city, string? state, string? postalCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            PostalCode = postalCode;
        }
    }
}
