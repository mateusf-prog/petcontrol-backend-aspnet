using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class PetSupportMapper
    {
        public static PetSupport ToEntity(this PetSupportDto dto)
        {
            return new PetSupport(
                dto.Name,
                dto.SmallDogPrice,
                dto.MediumDogPrice,
                dto.LargeDogPrice,
                null);
        }
        public static PetSupportDto ToDto(this PetSupport entity)
        {
            return new PetSupportDto
            {
                Id = entity.Id,
                Name = entity.Name,
                SmallDogPrice = entity.SmallDogPrice,
                MediumDogPrice = entity.MediumDogPrice,
                LargeDogPrice = entity.LargeDogPrice
            };
        }
    }
}
