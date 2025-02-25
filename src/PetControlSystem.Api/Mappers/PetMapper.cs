using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Enums;

namespace PetControlSystem.Api.Mappers
{
    public static class PetMapper
    {
        public static Pet ToEntity(this PetDto dto)
        {
            var petType = Enum.Parse<PetType>(dto.Type.ToString());
            var gender = Enum.Parse<Gender>(dto.Gender.ToString());

            return new Pet(
                dto.Name,
                dto.Description,
                dto.Weight,
                petType,
                gender,
                dto.Customer.ToEntity());
        }
        public static PetDto ToDto(this Pet entity)
        {
            return new PetDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Weight = entity.Weight,
                Type = (int)entity.Type,
                Gender = (int)entity.Gender,
                Customer = entity.Customer.ToDto()
            };
        }
    }
}
