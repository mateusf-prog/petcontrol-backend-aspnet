using Microsoft.IdentityModel.Tokens;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class PetSupportService : BaseService, IPetSupportService
    {
        private readonly IPetSupportRepository _repository;

        public PetSupportService(IPetSupportRepository repository,
                                  INotificator notificator) : base(notificator)
        {
            _repository = repository;
        }

        public async Task Add(PetSupport input)
        {
            if(!ExecuteValidation(new PetSupportValidation(), input)) return;

            if (await _repository.GetById(input.Id) != null)
            {
                Notify("There is already a pet with this ID");
                return;
            }

            var petSupport = await _repository.Get(ps => ps.Name == input.Name);

            if (!petSupport.IsNullOrEmpty())
            {
                Notify("There is already a pet with this name");
                return;
            }

            await _repository.Add(input);
        }

        public async Task Update(Guid id, PetSupport input)
        {
            if (!ExecuteValidation(new PetSupportValidation(), input)) return;

            var result = await _repository.GetById(id);

            if (result is null)
            {
                Notify("PetSupport not found");
                return;
            }

            result.Update(input.Name, input.SmallDogPrice, input.MediumDogPrice, input.LargeDogPrice, input.Appointments);

            await _repository.Update(result);
        }

        public async Task Delete(Guid id)
        {
            if (await _repository.GetById(id) is null)
            {
                Notify("PetSupport not found");
                return;
            }

            await _repository.Remove(id);
        }

        public async Task<List<PetSupport>> GetPetSupportsByIds(List<Guid> guids)
        {
            var petSupports = new List<PetSupport>();
            foreach (Guid id in guids)
            {
                var petSupport = await _repository.GetById(id);
                if (petSupport is null)
                {
                    Notify($"Service not found - ID {id}");
                    return petSupports;
                }
                petSupports.Add(petSupport);
            }
            return petSupports;
        }

        public void Dispose()
        {
            _repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
