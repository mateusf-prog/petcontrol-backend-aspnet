using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

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
                Description = entity.Description,
                Weight = entity.Weight,
                CustomerId = entity.CustomerId
            };
        }
    }
}
