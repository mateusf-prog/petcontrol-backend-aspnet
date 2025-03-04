using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Enums;

namespace PetControlSystem.Api.Mappers
{
    public static class PetMapper
    {
        public static Pet ToEntity(this PetDto dto)
        {
            return new Pet(
                dto.Name,
                dto.Description,
                dto.Weight,
                dto.CustomerId);
        }
        public static PetDto ToDto(this Pet entity)
        {
            return new PetDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Weight = entity.Weight,
                CustomerId = entity.Customer!.Id
            };
        }
    }
}
