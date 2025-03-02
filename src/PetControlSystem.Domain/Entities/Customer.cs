namespace PetControlSystem.Domain.Entities
{
    public class Customer : Entity
    {
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Phone { get; private set; }
        public string? Document { get; private set; }

        /* EF Relations */
        public Address? Address { get; private set; }
        public List<Order>? Orders { get; private set; }
        public List<Appointment>? Appointments { get; private set; }
        public List<Pet>? Pets { get; private set; }

        public Customer() { }

        public Customer(string? name, string? email, string? phone, string? document, Address? address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
            Address = address;
        }
    }
}
