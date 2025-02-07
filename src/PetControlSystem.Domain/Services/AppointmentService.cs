using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Domain.Services
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task Add(Appointment appointment)
        {
            if (!ExecuteValidation(new AppointmentValidation(), appointment)) return;

            if (_appointmentRepository.GetById(appointment.Id) != null)
            { 
                Notify("There is already an appointment with this ID");
                return;
            }

            await _appointmentRepository.Add(appointment);
        }

        public async Task Delete(Guid id)
        {
            if (_appointmentRepository.GetById(id) is null)
            {
                Notify("There is no appointment with this ID");
                return;
            }

            await _appointmentRepository.Remove(id);
        }

        public async Task Update(Appointment appointment)
        {
            if (!ExecuteValidation(new AppointmentValidation(), appointment)) return;

            var result = _appointmentRepository.GetById(appointment.Id);
            if (result != null) 
            {
                Notify("There is no appointment with this ID");
                return;
            }

            await _appointmentRepository.Update(appointment);
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            var result = await _appointmentRepository.GetById(id);

            if (result is null)
            {
                Notify("There is no appointment with this ID");
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<Appointment>?> GetAll()
        {
            var result = await _appointmentRepository.GetAll();

            if (result is null)
            {
                Notify("There are no appointments");
                return null;
            }

            return result;
        }

        public void Dispose()
        {
            _appointmentRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
