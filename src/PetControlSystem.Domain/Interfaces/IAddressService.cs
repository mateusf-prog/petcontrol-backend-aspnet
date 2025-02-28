using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IAddressService : IDisposable
    {
        Task Add(Address address);
        Task Update(Address address);
        Task Remove(Guid id);
    }
}