using Microsoft.AspNetCore.Identity;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(IdentityUser user);
        Task<string> Login(string email, string password);
    }
}