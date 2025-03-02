using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class PetService : BaseService, IPetService
    {
        private readonly IPetRepository _repository;

        public PetService(IPetRepository repository, INotificator notification) : base(notification)
        {
            _repository = repository;
        }

        public async Task Add(Pet pet)
        {
            if (!ExecuteValidation(new PetValidation(), pet)) return;

            if (_repository.GetById(pet.Id) != null)
            {
                Notify("There is already a pet with this ID");
                return;
            }

            await _repository.Add(pet);
        }

        public async Task Update(Guid id, Pet pet)
        {
            if (!ExecuteValidation(new PetValidation(), pet)) return;

            if (await _repository.GetById(pet.Id) == null)
            {
                Notify("Pet not found");
                return;
            }

            await _repository.Update(pet);
        }

        public async Task Delete(Pet pet)
        {
            if (await _repository.GetById(pet.Id) == null)
            {
                Notify("Pet not found");
                return;
            }

            await _repository.Remove(pet.Id);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
