using Microsoft.EntityFrameworkCore;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class PetSupportRepository : Repository<PetSupport>, IPetSupportRepository
    {
        public PetSupportRepository(DbContext db) : base(db) { }
    }
}
