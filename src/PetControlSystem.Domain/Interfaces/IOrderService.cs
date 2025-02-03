using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task Add(Order order);
        Task Delete(Order order);
    }
}
