using Microsoft.AspNetCore.Identity;
using PetControlSystem.Api.Dto;

namespace PetControlSystem.Api.Mappers
{
    public static class UserMapper
    {
        public static IdentityUser ToEntity(this UserDto userDto)
        {
            return new IdentityUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                NormalizedEmail = userDto.Email?.ToUpper(),
                PhoneNumber = userDto.Phone,
                PasswordHash = userDto.Password
            };
        }

        public static IdentityUser ToEntity(this UserLoginDto userLoginDto)
        {
            return new IdentityUser
            {
                UserName = userLoginDto.Email,
                PasswordHash = userLoginDto.Password,
                NormalizedEmail = userLoginDto.Email?.ToUpper()
            };
        }
    }
}