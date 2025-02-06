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

            await _appointmentRepository.Add(appointment);
        }

        public async Task Delete(Guid id)
        {
            await _appointmentRepository.Remove(id);
        }

        public async Task Update(Appointment appointment)
        {
            if (!ExecuteValidation(new AppointmentValidation(), appointment)) return;

            await _appointmentRepository.Update(appointment);
        }

        public async Task<Appointment> GetById(Guid id)
        {
            await _appointmentRepository.GetById(id);
            return null;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            await _appointmentRepository.GetAll();
            return null;
        }

        public void Dispose()
        {
            _appointmentRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
