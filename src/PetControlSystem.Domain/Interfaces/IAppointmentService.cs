using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IAppointmentService : IDisposable
    {
        Task Add(Appointment appointment);
        Task Update(Guid id, Appointment appointment);
        Task Delete(Guid id);
    }
}
