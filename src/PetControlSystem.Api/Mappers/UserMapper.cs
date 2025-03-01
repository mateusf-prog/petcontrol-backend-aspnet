using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Enums;
using PetControlSystem.Domain.Utils;

namespace PetControlSystem.Api.Mappers
{
    public static class UserMapper
    {
        public static User ToEntity(this UserDto dto)
        {
            var id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id;
            var documentType = Enum.Parse<DocumentType>(dto.DocumentType.ToString());
            var userType = Enum.Parse<UserType>(dto.Type.ToString());
            var password = PasswordUtils.EncryptPassword(dto.Password!);

            return new User(
                id,
                dto.Name,
                dto.Email,
                password,
                dto.Phone,
                dto.Document,
                documentType,
                userType,
                dto.AddressDto.ToEntity());
        }

        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                Document = user.Document,
                DocumentType = (int)user.DocumentType,
                Type = (int)user.Type
            };
        }
    }
}
