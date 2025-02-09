namespace PetControlSystem.Domain.Entities
{
    public class Product : Entity
    {
        public string? Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string? Description { get; private set; }

        /* EF Relations */
        public List<Order>? Orders { get; private set; } = [];
    }
}
