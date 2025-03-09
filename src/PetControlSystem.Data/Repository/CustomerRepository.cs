using Microsoft.EntityFrameworkCore;
using PetControlSystem.Data.Context;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyDbContext context) : base(context) { }

        public async Task<Customer?> GetCustomerWithPets(Guid id)
        {
            return await Db.Set<Customer>()
                .Include(c => c.Pets)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
