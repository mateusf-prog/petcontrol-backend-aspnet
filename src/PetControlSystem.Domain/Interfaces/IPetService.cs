using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IPetService : IDisposable
    {
        Task Add(Pet pet);
        Task Update(Guid id, Pet pet);
        Task Delete(Guid id);
    }
}
