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
            if (!ExecuteValidation(new CustomerValidation(), customer)) return;

            if (_repository.Get(c => c.Document == customer.Document).Result.Any())
            {
                Notify("There is already a customer with this document");
                return;
            }

            if (_repository.Get(c => c.Email == customer.Email).Result.Any())
            {
                Notify("There is already a customer with this email");
                return;
            }

            await _repository.Add(customer);
        }

        public async Task Update(Guid id, Customer input)
        {
            if (!ExecuteValidation(new CustomerValidation(), input)) return;

            var result = await _repository.GetById(id);

            if (result is null)
            {
                Notify("Customer not found");
                return;
            }

            result.Update(input.Name!, 
                input.Email!, 
                input.Phone!, 
                input.Document!, 
                input.Address);

            await _repository.Update(result);
        }

        public async Task Delete(Guid id)
        {
            var customer = await _repository.GetById(id);
            if (customer is null)
            {
                Notify("Customer not found");
                return;
            }

            // Set Address to null before deleting
            customer.Update(customer.Name!, customer.Email!, customer.Phone!, customer.Document!, null);
            await _repository.Update(customer);

            await _repository.Remove(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
