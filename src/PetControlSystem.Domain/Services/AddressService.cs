using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly IAddressRepository _repository;

        public AddressService(IAddressRepository repository, INotificator notification) : base(notification)
        {
            _repository = repository;
        }

        public async Task Add(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            if (_repository.GetById(address.Id) != null)
            {
                Notify("There is already an address with this ID");
                return;
            }

            await _repository.Add(address);
        }

        public async Task Remove(Guid id)
        {
            if (_repository.GetById(id) is null)
            {
                Notify("There is no address with this ID");
                return;
            }
            await _repository.Remove(id);
        }

        public async Task Update(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            var result = _repository.GetById(address.Id);

            if (result != null)
            {
                Notify("There is no address with this ID");
                return;
            }

            await _repository.Update(address);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
