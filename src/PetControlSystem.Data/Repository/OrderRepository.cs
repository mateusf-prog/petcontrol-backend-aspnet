using Microsoft.EntityFrameworkCore;
using PetControlSystem.Data.Context;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MyDbContext db) : base(db) { }

        public Task<List<Order>> GetAllOrdersWithProducts()
        {
            return Db.Set<Order>()
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdWithProducts(Guid id)
        {
            return await Db.Set<Order>()
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
