using PetControlSystem.Domain.Entities;

namespace IntegratedTests.Seed
{
    public static class FakerData
    {
        public static Customer GetValidFakerCustomer()
        {            
            return new Customer("test-name", "test-email@email.com", "12312312311", "00011122200", GetValidFakerAddress());
        }

        public static Address GetValidFakerAddress()
        {
            return new Address("street", "170", "complement", "neighborhood", "city", "SP", "12212700");
        }

        public static Product GetValidFakerProduct()
        {
            return new Product("test-product", 10.0m, 250, "product-description");
        }
    }
}
