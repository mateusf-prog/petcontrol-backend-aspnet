using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class CustomerMapper
    {
        public static Customer ToEntity(this CustomerDto dto)
        {
            return new Customer(
                dto.Name,
                dto.Email,
                dto.Phone,
                dto.Document,
                null);
        }
        public static CustomerDto ToDto(this Customer entity)
        {
            return new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
            };
        }
    }
}
