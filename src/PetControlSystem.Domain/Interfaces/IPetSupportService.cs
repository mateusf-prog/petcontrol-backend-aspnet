using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IPetSupportService : IDisposable
    {
        Task Add(PetSupport petSupport);
        Task Update(PetSupport petSupport);
        Task Delete(PetSupport petSupport);
    }
}
