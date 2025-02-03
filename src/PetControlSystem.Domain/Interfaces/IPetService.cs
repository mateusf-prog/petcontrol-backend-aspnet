using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IPetService : IDisposable
    {
        Task Add(Pet pet);
        Task Update(Pet pet);
        Task Delete(Pet pet);
    }
}
