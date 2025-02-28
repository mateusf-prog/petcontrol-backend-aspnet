using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task Add(User user);
        Task<dynamic> Login(User user);
        Task<User> Update(User user);
        Task UpdatePassword(User user, string newPassword);
    }
}
