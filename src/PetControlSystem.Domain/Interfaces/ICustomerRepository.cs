using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<Pet>> GetPetsFromCustomer(Guid customerId);
    }
}
