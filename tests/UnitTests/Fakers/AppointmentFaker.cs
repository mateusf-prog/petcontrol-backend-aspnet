using Bogus;
using PetControlSystem.Domain.Entities;

namespace UnitTests.Fakers
{
    public static class AppointmentFaker
    {
        public static Appointment GetValidAppointmentFaker()
        {
            var appointmentPetSupports = new Faker<AppointmentPetSupport>().Generate(2);
            
            return new Faker<Appointment>()
                .RuleFor(a => a.PetId, Guid.NewGuid)
                .RuleFor(a => a.CustomerId, Guid.NewGuid())
                .RuleFor(a => a.Date, DateTime.Now.AddDays(1))
                .RuleFor(a => a.AppointmentPetSupports, appointmentPetSupports)
                .Generate();
        }
    }

}
