using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task Register(User user);
        Task<dynamic> Login(string email, string password);
        Task Update(Guid id, User user);
        Task Delete(Guid id);
    }
}
