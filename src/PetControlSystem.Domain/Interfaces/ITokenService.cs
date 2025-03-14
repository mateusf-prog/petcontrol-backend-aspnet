using Microsoft.AspNetCore.Identity;

namespace PetControlSystem.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(IdentityUser user);
    }
}