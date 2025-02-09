using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository,
                            INotificator notificator) : base(notificator)
        {
            _orderRepository = orderRepository;
        }

        public async Task Add(Order order)
        {
            if (!ExecuteValidation(new OrderValidation(), order)) return;

            if (_orderRepository.GetById(order.Id) != null) 
            {
                Notify("There is already an order with this ID");
                return;
            }

            await _orderRepository.Add(order);
        }

        public async Task Delete(Guid id)
        {
            await _orderRepository.Remove(id);
        }

        public async Task<Order?> GetById(Guid id)
        {
            var result = await _orderRepository.GetById(id);

            if (result is null)
            {
                Notify("There is no order with this ID");
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<Order>?> GetAll()
        {
            var result = await _orderRepository.GetAll();

            if (result is null)
            {
                Notify("There are no orders");
                return null;
            }

            return result;
        }

        public void Dispose()
        {
            _orderRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
