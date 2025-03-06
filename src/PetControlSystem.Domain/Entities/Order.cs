namespace PetControlSystem.Domain.Entities
{
    public class Order : Entity
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public decimal TotalPrice { get; private set; }

        /* EF Relations */
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public List<Product> Products { get; private set; } = [];

        public Order() { }

        public Order(Guid customerId, List<Product> products)
        {
            CustomerId = customerId;
            Products = products;
            TotalPrice = products.Sum(p => p.Price);
        }

        public void Update(Customer customer, List<Product> products)
        {
            Customer = customer;
            Products = products;
            TotalPrice = products.Sum(p => p.Price);
        }
    }
}
