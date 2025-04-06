using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetControlSystem.Api.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace IntegratedTests.Utils
{
    public static class AuthConfig
    {
        public static string GenerateToken(IdentityUser user)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings-integrated-tests.json")
                .Build();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JwtSecurity:Secret"]);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "PetControlSystem",
                Audience = "PetControlSystem",
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        public static IdentityUser GetValidUser()
        {
            return new IdentityUser()
            {
                Id = "test-user-id",
                UserName = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "Teste@Password123",
                NormalizedEmail = "testuser@example.com"
            };
        }

        public static UserDto GetValidUserDto()
        {
            return new UserDto()
            {
                Email = "testuser@example.com",
                Password = "Teste@Password123",
                ConfirmPassword = "Teste@Password123",
                Phone = "1234567890",
                UserName = "testuser"
            };
        }

        public static UserDto GetInvalidUserDto()
        {
            return new UserDto()
            {
                Email = "wrong-email",
                Password = "Teste@Password123",
                ConfirmPassword = "Teste@Password123",
                Phone = "1234567890",
                UserName = "testuser"
            };
        }
    }
}
