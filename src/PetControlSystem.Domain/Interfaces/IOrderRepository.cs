using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetByIdWithProducts(Guid id);
        Task<List<Order>> GetAllOrdersWithProducts();
    }    
}
