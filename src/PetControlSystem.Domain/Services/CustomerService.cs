using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository customerRepository,
                              INotificator notificator) : base(notificator)
        {
            _repository = customerRepository;
        }

        public async Task Add(Customer customer)
        {
            if (ExecuteValidation(new CustomerValidation(), customer)) return;

            if (_repository.Get(c => c.Document == customer.Document).Result.Any())
            {
                Notify("There is already a customer with this document");
                return;
            }

            if (_repository.Get(c => c.Document == customer.Email).Result.Any())
            {
                Notify("There is already a customer with this email");
                return;
            }

            await _repository.Add(customer);
        }

        public async Task Update(Guid id, Customer customer)
        {
            if (ExecuteValidation(new CustomerValidation(), customer)) return;
            
            if (_repository.GetById(customer.Id) is null)
            {
                Notify("Customer not found");
                return;
            }

            await _repository.Update(customer);
        }

        public async Task Delete(Guid id)
        {
            if (_repository.GetById(id) is null)
            {
                Notify("Customer not found");
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
