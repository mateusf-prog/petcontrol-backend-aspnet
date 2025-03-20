using Bogus;
using PetControlSystem.Domain.Entities;

namespace UnitTests.Fakers
{
    public static class OrderFaker
    {
        public static Order GetValidOrderFaker()
        {
            var orderProducts = new Faker<OrderProduct>()
            .RuleFor(p => p.Quantity, f => f.Random.Number(5, 10))
            .Generate(2);

            return new Order(
                Guid.NewGuid(), 
                orderProducts, 
                1500);
        }
    }
}