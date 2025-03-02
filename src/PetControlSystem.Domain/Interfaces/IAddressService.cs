using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IAddressService : IDisposable
    {
        Task Add(Address address);
        Task Update(Guid id, Address address);
        Task Delete(Guid id);
    }
}