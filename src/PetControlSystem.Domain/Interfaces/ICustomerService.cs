using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface ICustomerService : IDisposable
    {
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task<Customer?> GetById(Guid id);
        Task<IEnumerable<Customer>?> GetAll();
        Task Delete(Guid id);
    }
}
