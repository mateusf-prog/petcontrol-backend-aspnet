using PetControlSystem.Domain.Entities;

namespace IntegratedTests.Seed
{
    public class FakerData
    {
        public static Address GetAddress()
        {
            return new Address("street", "170", "complement", "neighborhood", "city", "SP", "12212700");
        }

        public static Customer GetCustomer()
        {
            var address = GetAddress();
            return new Customer("test-name", "test-email@email.com", "12312312311", "00011122200", address);
        }

        public static Product GetProduct()
        {
            return new Product("test-product", 10.0m, 250, "product-description");
        }

        public static Pet GetPet(Guid customerId)
        {
            var customer = GetCustomer();
            return new Pet("dog-name", "dog-description", 30.0, customerId);
        }
    }
}
