namespace PetControlSystem.Domain.Entities
{
    public class Order : Entity
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public decimal? TotalPrice { get; private set; }

        /* EF Relations */
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public List<OrderProduct> OrderProducts { get; private set; } = [];

        public Order() { }

        public Order(Guid customerId, List<OrderProduct> orderProducts, decimal? totalPrice)
        {
            CustomerId = customerId;
            OrderProducts = orderProducts;
            TotalPrice = totalPrice;
        }

        public void Update(List<OrderProduct> orderProducts)
        {
            OrderProducts = orderProducts;
            TotalPrice = orderProducts.Sum(x => x.Price * x.Quantity);
        }
    }
}
