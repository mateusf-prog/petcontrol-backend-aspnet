using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task Add(Order order);
        Task Delete(Guid id);
        Task Update(Guid id, Order order);
    }
}
