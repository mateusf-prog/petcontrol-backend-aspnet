using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class AddressMapper
    {
        public static Address ToEntity(this AddressDto dto)
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

        public static AddressDto ToDto(this Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
                Street = address.Street,
                Number = address.Number,
                Complement = address.Complement,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State,
                ZipCode = address.PostalCode
            };
        }
    }
}
