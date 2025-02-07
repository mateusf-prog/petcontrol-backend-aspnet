using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Domain.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Add(Customer customer)
        {
            if (ExecuteValidation(new CustomerValidation(), customer)) return;

            if (_customerRepository.Get(c => c.Document == customer.Document).Result.Any())
            {
                Notify("There is already a customer with this document");
                return;
            }

            if (_customerRepository.Get(c => c.Document == customer.Email).Result.Any())
            {
                Notify("There is already a customer with this email");
                return;
            }

            await _customerRepository.Add(customer);
        }

        public async Task Delete(Guid id)
        {
            if (_customerRepository.GetById(id) is null)
            {
                Notify("There is no customer with this ID");
                return;
            }

            await _customerRepository.Remove(id);
        }

        public async Task Update(Customer customer)
        {
            if (ExecuteValidation(new CustomerValidation(), customer)) return;
            
            if (_customerRepository.GetById(customer.Id) is null)
            {
                Notify("There is no customer with this ID");
                return;
            }

            await _customerRepository.Update(customer);
        }

        public async Task<Customer?> GetById(Guid id)
        {
            var result = await _customerRepository.GetById(id);

            if (result is null)
            {
                Notify("There is no customer with this ID");
                return null;
            }
;
            return result;
        }

        public async Task<IEnumerable<Customer>?> GetAll()
        {
            var result = await _customerRepository.GetAll();

            if (result is null)
            {
                Notify("There are no customers");
                return null;
            }

            return result;
        }

        public void Dispose()
        {
            _customerRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
