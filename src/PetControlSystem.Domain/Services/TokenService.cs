using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PetControlSystem.Domain.Security
{
    public class TokenService : BaseService, ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration, INotificator notificator) : base(notificator)
        {
            _configuration = configuration;
        }

        public string GenerateToken(IdentityUser user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSecurity:Secret"]);
                var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _configuration["JwtSecurity:Issuer"],
                    Audience = _configuration["JwtSecurity:Audience"],
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                });

                return tokenHandler.WriteToken(token);
            } 
            catch (Exception)
            {
                Notify("Error generating token");
                return string.Empty;
            }
        }
    }
}
