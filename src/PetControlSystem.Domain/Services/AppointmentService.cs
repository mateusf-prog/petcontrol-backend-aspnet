using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPetSupportService _petSupportService;

        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  INotificator notificator,
                                  ICustomerRepository customerRepository,
                                  IPetSupportService petSupportService) : base(notificator)
        {
            _repository = appointmentRepository;
            _customerRepository = customerRepository;
            _petSupportService = petSupportService;
        }

        public async Task Add(Appointment input)
        {
            if (!ExecuteValidation(new AppointmentValidation(), input)) return;

            var customer = await _customerRepository.GetCustomerWithPets(input.CustomerId);
            if (customer is null)
            {
                Notify("Customer not found");
                return;
            }

            if (!customer.Pets.Select(p => p.Id == input.PetId).FirstOrDefault())
            {
                Notify("Pet not found");
                return;
            }

            var petSupportIds = input.AppointmentPetSupports.Select(ps => ps.PetSupportId).ToList();
            var petSupports = await _petSupportService.GetPetSupportsByIds(petSupportIds);

            if (petSupports.Count != petSupportIds.Count)
            {
                Notify("One or more PetSupports not found");
                return;
            }

            await _repository.Add(input);
        }

        public async Task Update(Guid id, Appointment input)
        {
            if (!ExecuteValidation(new AppointmentValidation(), input)) return;

            var result = await _repository.GetByIdWithPetSupport(id);
            if (result is null)
            {
                Notify("Appointment not found");
                return;
            }

            var petSupportIds = input.AppointmentPetSupports.Select(ps => ps.PetSupportId).ToList();
            var petSupports = await _petSupportService.GetPetSupportsByIds(petSupportIds);
            if (petSupports.Count != petSupportIds.Count)
            {
                Notify("One or more PetSupports not found");
                return;
            }

            foreach (var petSupport in result.AppointmentPetSupports.ToList())
            {
                result.AppointmentPetSupports.Remove(petSupport);
            }

            foreach (var petSupport in input.AppointmentPetSupports)
            {
                var newPetSupport = new AppointmentPetSupport(
                        petSupport.PetSupportId,
                        result.Id, 
                        petSupport.Price);

                result.AppointmentPetSupports.Add(newPetSupport);
            }

            result.Update(input.Date, input.Description, input.TotalPrice, result.AppointmentPetSupports);

            await _repository.Update(result);
        }

        public async Task Delete(Guid id)
        {
            if (await _repository.GetById(id) is null)
            {
                Notify("Appointment not found");
                return;
            }

            await _repository.Remove(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
