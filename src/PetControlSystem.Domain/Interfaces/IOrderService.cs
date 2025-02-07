using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task Add(Order order);
        Task Delete(Guid id);
        Task<Order?> GetById(Guid id);
        Task<IEnumerable<Order>?> GetAll();
    }
}
