using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task GetById(Guid id);
        Task GetAll();
        Task Add(User user);
        Task Login(User user);
        Task Update(Guid id, User user);
        Task Delete(Guid id);
    }
}
