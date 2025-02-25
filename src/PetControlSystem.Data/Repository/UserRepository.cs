using Microsoft.EntityFrameworkCore;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext db) : base(db) { }
    }
}
