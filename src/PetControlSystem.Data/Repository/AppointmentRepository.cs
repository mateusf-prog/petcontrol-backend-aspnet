using Microsoft.EntityFrameworkCore;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DbContext db) : base(db) { }
    }
}
