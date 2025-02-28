using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<User> GetById(Guid id);
        Task<IEnumerable<User>> GetAll();
        Task<User> Add(User user);
        Task<dynamic> Login(User user);
        Task<User> Update(Guid id, User user);
        Task<dynamic> Delete(Guid id);
        Task UpdatePassword(User user, string newPassword);
    }
}
