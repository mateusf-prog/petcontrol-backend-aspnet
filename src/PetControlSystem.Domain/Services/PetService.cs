using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class PetService : BaseService, IPetService
    {
        private readonly IPetRepository _repository;
        private readonly ICustomerRepository _customerRepository;

        public PetService(IPetRepository repository, ICustomerRepository customerRepository, INotificator notification) : base(notification)
        {
            _repository = repository;
            _customerRepository = customerRepository;
        }

        public async Task Add(Pet pet)
        {
            if (!ExecuteValidation(new PetValidation(), pet)) return;

            if (await _customerRepository.GetById(pet.CustomerId) is null)
            {
                Notify("Customer not found");
                return;
            }

            if (await _repository.GetById(pet.Id) != null)
            {
                Notify("There is already a pet with this ID");
                return;
            }

            await _repository.Add(pet);
        }

        public async Task Update(Guid id, Pet input)
        {
            if (!ExecuteValidation(new PetValidation(), input)) return;

            var result = await _repository.GetById(id);

            if (result is null)
            {
                Notify("Pet not found");
                return;
            }

            if (await _customerRepository.GetById(input.CustomerId) is null)
            {
                Notify("Customer not found");
                return;
            }

            result.Update(input.Name!, 
                input.Description!, 
                input.Weight, 
                input.CustomerId);

            await _repository.Update(result);
        }

        public async Task Delete(Guid id)
        {
            if (await _repository.GetById(id) == null)
            {
                Notify("Pet not found");
                return;
            }

            await _repository.Remove(id);
        }

        public void Dispose()
        {
            _repository.Dispose();
            _customerRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
