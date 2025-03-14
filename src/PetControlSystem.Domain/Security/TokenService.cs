using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetControlSystem.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PetControlSystem.Domain.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSecurity:Secret"]);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _configuration["JwtSecurity:Issuer"],
                Audience = _configuration["JwtSecurity:Audience"],
                Expires = System.DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
