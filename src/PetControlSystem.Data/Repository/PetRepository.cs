using PetControlSystem.Data.Context;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        public PetRepository(MyDbContext db) : base(db) { }
    }
}
