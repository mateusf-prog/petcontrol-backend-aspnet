using PetControlSystem.Data.Context;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class PetSupportRepository : Repository<PetSupport>, IPetSupportRepository
    {
        public PetSupportRepository(MyDbContext db) : base(db) { }
    }
}
