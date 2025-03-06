using Microsoft.EntityFrameworkCore;
using PetControlSystem.Data.Context;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(MyDbContext context) : base(context)
        {
        }
    }
}
