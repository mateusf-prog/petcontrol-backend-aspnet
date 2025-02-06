using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Domain.Services
{
    public class PetSuppportService : BaseService, IPetSupportService
    {
        private readonly IPetSupportRepository _petSupportRepository;

        public PetSuppportService(IPetSupportRepository petSupportRepository)
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
            await _petSupportRepository.Remove(id);
        }

        public async Task<IEnumerable<PetSupport>> GetAll()
        {
            await _petSupportRepository.GetAll();
            return null;
        }

        public async Task<PetSupport> GetById(Guid id)
        {
            await _petSupportRepository.GetById(id);
            return null;
        }

        public async Task Update(PetSupport petSupport)
        {
            if (!ExecuteValidation(new PetSupportValidation(), petSupport)) return;

            await _petSupportRepository.Update(petSupport);
        }

        public void Dispose()
        {
            _petSupportRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
