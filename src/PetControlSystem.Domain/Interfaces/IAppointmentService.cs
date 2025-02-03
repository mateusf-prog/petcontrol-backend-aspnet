using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IAppointmentService : IDisposable
    {
        Task Add(Appointment appointment);
        Task Update(Appointment appointment);
        Task Delete(Appointment appointment);
    }
}
