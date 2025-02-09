using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class PetSuppportService : BaseService, IPetSupportService
    {
        private readonly IPetSupportRepository _petSupportRepository;

        public PetSuppportService(IPetSupportRepository petSupportRepository,
                                  INotificator notificator) : base(notificator)
        {
            _petSupportRepository = petSupportRepository;
        }

        public async Task Add(PetSupport petSupport)
        {
            if(!ExecuteValidation(new PetSupportValidation(), petSupport)) return;

            await _petSupportRepository.Add(petSupport);
        }

        public async Task Delete(Guid id)
        {
            if (_petSupportRepository.GetById(id) is null) 
            {
                Notify("There is no pet support with this ID");
                return;
            }

            await _petSupportRepository.Remove(id);
        }

        public async Task<IEnumerable<PetSupport>?> GetAll()
        {
            var result = await _petSupportRepository.GetAll();

            if (result is null)
            {
                Notify("There are no pet supports");
                return null;
            }

            return result;
        }

        public async Task<PetSupport?> GetById(Guid id)
        {
            var result = await _petSupportRepository.GetById(id);

            if (result is null)
            {
                Notify("There is no pet support with this ID");
                return null;
            }

            return result;
        }

        public async Task Update(PetSupport petSupport)
        {
            if (!ExecuteValidation(new PetSupportValidation(), petSupport)) return;

            var result = await _petSupportRepository.GetById(petSupport.Id);

            if (result is null)
            {
                Notify("There is no pet support with this ID");
                return;
            }

            await _petSupportRepository.Update(petSupport);
        }

        public void Dispose()
        {
            _petSupportRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
