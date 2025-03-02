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

        public async Task Add(Appointment appointment)
        {
            if (!ExecuteValidation(new AppointmentValidation(), appointment)) return;

            if (_repository.GetById(appointment.Id) != null)
            { 
                Notify("There is already an appointment with this ID");
                return;
            }

            await _repository.Add(appointment);
        }

        public async Task Update(Guid id, Appointment appointment)
        {
            if (!ExecuteValidation(new AppointmentValidation(), appointment)) return;

            var result = _repository.GetById(appointment.Id);
            if (result != null)
            {
                Notify("Appointment not found");
                return;
            }

            await _repository.Update(appointment);
        }

        public async Task Delete(Guid id)
        {
            if (_repository.GetById(id) is null)
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
