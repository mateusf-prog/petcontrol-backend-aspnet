using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Enums;

namespace PetControlSystem.Api.Mappers
{
    public static class UserMapper
    {
        public static User ToEntity(this UserDto dto)
        {
            var documentType = Enum.Parse<DocumentType>(dto.DocumentType.ToString());
            var userType = Enum.Parse<UserType>(dto.Type.ToString());

            return new User(
                dto.Name,
                dto.Email,
                dto.Password,
                dto.Phone,
                dto.Document,
                documentType,
                userType,
                null);
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
                Type = (int)user.Type,
            };
        }
    }
}
