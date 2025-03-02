using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class AddressMapper
    {
        public static Address ToValueObject(this AddressDto dto)
        {
            return new Address(
                dto.Street,
                dto.Number,
                dto.Complement,
                dto.Neighborhood,
                dto.City,
                dto.State,
                dto.ZipCode);
        }

        public static AddressDto ToDto(this Address entity)
        {
            return new AddressDto
            {
                Street = entity.Street,
                Number = entity.Number,
                Complement = entity.Complement,
                Neighborhood = entity.Neighborhood,
                City = entity.City,
                State = entity.State,
                ZipCode = entity.PostalCode
            };
        }
    }
}
