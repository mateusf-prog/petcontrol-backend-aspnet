using Microsoft.EntityFrameworkCore;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        public PetRepository(DbContext db) : base(db) { }
    }
}
