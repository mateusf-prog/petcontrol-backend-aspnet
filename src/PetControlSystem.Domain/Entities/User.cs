using PetControlSystem.Domain.Entities.Enums;

namespace PetControlSystem.Domain.Entities
{
    public class User : Entity
    {
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public string? Phone { get; private set; }
        public string? Document { get; private set; }
        public DocumentType DocumentType { get; private set; }
        public UserType Type { get; private set; }

        /* EF Relations */
        public Guid AddressId { get; private set; }
        public Address? Address { get; private set; }

        public User(string? name, string? email, string? password, string? phone, string? document, DocumentType documentType, UserType type, Address? address)
        {
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            Document = document;
            DocumentType = documentType;
            Type = type;
            Address = address;
        }
    }
}
