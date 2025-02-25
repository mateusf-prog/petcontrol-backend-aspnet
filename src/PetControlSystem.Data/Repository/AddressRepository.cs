using Microsoft.EntityFrameworkCore;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DbContext db) : base(db) { }
    }
}
