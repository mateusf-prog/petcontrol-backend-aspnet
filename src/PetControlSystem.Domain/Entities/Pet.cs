using PetControlSystem.Domain.Entities.Enums;
using System.Numerics;
using System.Reflection.Metadata;

namespace PetControlSystem.Domain.Entities
{
    public class Pet : Entity
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public double? Weight { get; private set; }

        /* EF Relations */
        public Guid CustomerId { get; private set; }
        public Customer? Customer { get; private set; }

        public Pet() { }

        public Pet(string? name, string? description, double? weight, Guid customerId)
        {
            Name = name;
            Description = description;
            Weight = weight;
            CustomerId = customerId;
        }

        public void Update(string name, string description, double? weight, Guid customerId)
        {
            Name = name;
            Description = description;
            Weight = weight;
            CustomerId = customerId;
        }
    }
}
