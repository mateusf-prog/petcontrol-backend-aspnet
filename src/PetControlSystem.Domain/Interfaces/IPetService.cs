using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IPetService : IDisposable
    {
        Task Add(Pet pet);
        Task GetById(Guid id);
        Task Update(Pet pet);
        Task Delete(Pet pet);
    }
}
