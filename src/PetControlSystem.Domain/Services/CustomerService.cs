using PetControlSystem.Domain.Entities;
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
            await _customerRepository.Add(customer);
        }

        public async Task Delete(Guid id)
        {
            await _customerRepository.Remove(id);
        }

        public async Task Update(Customer customer)
        {
            await _customerRepository.Update(customer);
        }

        public async Task GetById(Guid id)
        {
            await _customerRepository.GetById(id);
        }

        public void Dispose()
        {
            _customerRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
