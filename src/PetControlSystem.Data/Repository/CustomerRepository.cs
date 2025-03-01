using Microsoft.EntityFrameworkCore;
using PetControlSystem.Data.Context;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyDbContext context) : base(context) { }

        public async Task<List<Pet>> GetPetsFromCustomer(Guid customerId)
        {
            return await Db.Set<Pet>().Where(p => p.CustomerId == customerId).ToListAsync();
        }
    }
}
