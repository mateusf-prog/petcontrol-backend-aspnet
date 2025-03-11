using Microsoft.EntityFrameworkCore;
using PetControlSystem.Data.Context;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(MyDbContext db) : base(db) { }

        public Task<List<Appointment>> GetAlllAppointmentsWithPetSupports()
        {
            return Db.Set<Appointment>()
                .Include(a => a.AppointmentPetSupports)
                .ToListAsync();
        }

        public Task<Appointment> GetByIdWithPetSupport(Guid id)
        {
            return Db.Set<Appointment>()
                .Include(a => a.AppointmentPetSupports)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
