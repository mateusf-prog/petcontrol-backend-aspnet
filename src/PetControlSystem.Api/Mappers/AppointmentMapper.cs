using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class AppointmentMapper
    {
        public static Appointment ToEntity(this AppointmentDto dto)
        {
                return new Appointment(
                dto.Date,
                dto.Description,
                dto.Customer.Id,
                dto.Services.Select(s => s.ToEntity()).ToList());
                
        }
        public static AppointmentDto ToDto(this Appointment entity)
        {
            return new AppointmentDto
            {
                Id = entity.Id,
                Date = entity.Date,
                Description = entity.Description,
                Customer = entity.Customer.ToDto(),
                Services = entity.PetSupports.Select(s => s.ToDto()).ToList()
            };
        }
    }
}
