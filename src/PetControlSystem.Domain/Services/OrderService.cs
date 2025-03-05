using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository,
                            INotificator notificator) : base(notificator)
        {
            _repository = repository;
        }

        public async Task Add(Order input)
        {
            if (!ExecuteValidation(new OrderValidation(), input)) return;

            if (await _repository.GetById(input.Id) != null) 
            {
                Notify("There is already an order with this ID");
                return;
            }

            await _repository.Add(input);
        }

        public async Task Delete(Guid id)
        {
            if (await _repository.GetById(id) is null)
            {
                Notify("Order not found");
                return;
            }

            await _repository.Remove(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
