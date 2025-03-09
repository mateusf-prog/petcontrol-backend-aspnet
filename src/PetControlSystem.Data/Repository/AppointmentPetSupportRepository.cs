using PetControlSystem.Data.Context;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Data.Repository
{
    public class AppointmentPetSupportRepository : Repository<AppointmentPetSupport>, IAppointmentPetSupportRepository
    {
        public AppointmentPetSupportRepository(MyDbContext context) : base(context)
        {
        }
    }
}
