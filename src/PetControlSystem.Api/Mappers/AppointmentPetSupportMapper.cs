using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class AppointmentPetSupportMapper
    {
        public static AppointmentPetSupport ToEntity(this AppointmentPetSupportDto dto)
        {
            return new AppointmentPetSupport(
                dto.AppointmentId,
                dto.PetSupportId,
                dto.Price);
        }

        public static AppointmentPetSupportDto ToDto(this AppointmentPetSupport entity)
        {
            return new AppointmentPetSupportDto
            {
                Id = entity.Id,
                PetSupportId = entity.PetSupportId,
                Price = entity.Price,
            };
        }
    }
}
