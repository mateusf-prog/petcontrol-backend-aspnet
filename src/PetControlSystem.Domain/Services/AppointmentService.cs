using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  INotificator notificator) : base(notificator)
        {
            _repository = appointmentRepository;
        }

        public async Task Add(Appointment input)
        {
            if (!ExecuteValidation(new AppointmentValidation(), input)) return;

            if (await _repository.GetById(input.Id) != null)
            { 
                Notify("There is already an appointment with this ID");
                return;
            }

            await _repository.Add(input);
        }

        public async Task Update(Guid id, Appointment input)
        {
            if (!ExecuteValidation(new AppointmentValidation(), input)) return;

            var result = await _repository.GetById(input.Id);
            if (result is null)
            {
                Notify("Appointment not found");
                return;
            }

            result.Update(input.Date, input.Description, input.CustomerId, input.PetSupports);

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
