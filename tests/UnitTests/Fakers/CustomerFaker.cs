using Bogus;
using PetControlSystem.Domain.Entities;

namespace UnitTests.Fakers
{
    public static class CustomerFaker
    {
        public static Customer GetValidCustomerFaker()
        {
            return new Customer(
                new Faker().Name.FullName(),
                new Faker().Internet.Email(),
                new Faker().Random.String2(11),
                new Faker().Random.String2(12),
                null
            );
        }
    }
}