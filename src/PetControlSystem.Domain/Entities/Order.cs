namespace PetControlSystem.Domain.Entities
{
    public class Order : Entity
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public decimal TotalPrice { get; private set; }
        public Customer? Customer { get; private set; }
        public List<Product>? Products { get; private set; }
        public Status? Status { get; private set; }
    }
}
