using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IPetSupportService : IDisposable
    {
        Task Add(PetSupport petSupport);
        Task Update(Guid id, PetSupport petSupport);
        Task Delete(Guid id);
    }
}
