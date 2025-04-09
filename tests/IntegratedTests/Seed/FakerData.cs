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

        public static Order GetOrder(Guid customerId)
        {
            return new Order(customerId, [], 1500.00m);
        }

        public static Appointment GetAppointment(Guid customerId, Guid petId)
        {
            return new Appointment(DateTime.Now.AddDays(1), "some-description", 1500m, customerId, petId, []);
        }

        public static PetSupport GetPetSupport()
        {
            return new PetSupport("some-name", 50.0m, 75.0m, 100m, []);
        }
    }
}