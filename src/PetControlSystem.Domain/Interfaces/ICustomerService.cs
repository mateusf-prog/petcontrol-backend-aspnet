using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface ICustomerService : IDisposable
    {
        Task Add(Customer customer);
        Task Update(Guid id, Customer customer);
        Task Delete(Guid id);
    }
}
