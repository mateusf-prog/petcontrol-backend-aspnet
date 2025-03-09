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
            dto.TotalPrice,
            dto.CustomerId,
            dto.PetId,
            dto.PetSupports.Select(dto => dto.ToEntity()).ToList());
        }

        public static AppointmentDto ToDto(this Appointment entity)
        {
            return new AppointmentDto
            {
                Id = entity.Id,
                Date = entity.Date,
                Description = entity.Description,
                CustomerId = entity.CustomerId,
                PetId = entity.PetId,
                TotalPrice = entity.TotalPrice,
                PetSupports = entity.AppointmentPetSupports.Select(entity => entity.ToDto()).ToList()
            };
        }
    }
}
