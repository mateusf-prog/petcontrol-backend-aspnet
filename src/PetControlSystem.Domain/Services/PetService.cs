using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class PetService : BaseService, IPetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository repository, INotificator notification) : base(notification)
        {
            _petRepository = repository;
        }

        public async Task Add(Pet pet)
        {
            if (!ExecuteValidation(new PetValidation(), pet)) return;

            if (_petRepository.GetById(pet.Id) != null)
            {
                Notify("There is already a pet with this ID");
                return;
            }

            await _petRepository.Add(pet);
        }

        public Task GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Pet pet)
        {
            if (GetById(pet.Id) == null)
            {
                Notify("There is no pet with this ID");
                return;
            }

            await _petRepository.Remove(pet.Id);
        }

        public async Task Update(Pet pet)
        {
            if (!ExecuteValidation(new PetValidation(), pet)) return;

            if (GetById(pet.Id) == null)
            {
                Notify("There is no pet with this ID");
                return;
            }

            await _petRepository.Update(pet);
        }

        public void Dispose()
        {
            _petRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
