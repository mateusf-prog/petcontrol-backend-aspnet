using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Domain.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Add(Order order)
        {
            if (!ExecuteValidation(new OrderValidation(), order)) return;

            await _orderRepository.Add(order);
        }

        public async Task Delete(Guid id)
        {
            await _orderRepository.Remove(id);
        }

        public async Task<Order> GetById(Guid id)
        {
            await _orderRepository.GetById(id);
            return null;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            await _orderRepository.GetAll();
            return null;
        }

        public void Dispose()
        {
            _orderRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
