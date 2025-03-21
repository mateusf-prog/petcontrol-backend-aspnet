using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Security;
using UnitTests.Fakers;

namespace UnitTests.Domain.Services
{
    public class TokenServiceTest
    {
        private readonly TokenService _service;
        private readonly Mock<IConfiguration> _configuration = new();
        private readonly Notificator _notificator = new();

        public TokenServiceTest() =>
            _service = new TokenService(_configuration.Object, _notificator);


        [Fact]
        public void GenerateToken_WhenCalled_ShouldReturnToken()
        {
            // Arrange
            var user = IdentityUserFaker.GetIdentityUser();
            var secret = Guid.NewGuid().ToString();

            _configuration.SetupSequence(c => c["JwtSecurity:Secret"])
                .Returns(secret)
                .Returns("myIssuer")
                .Returns("myAudience");

            // Act
            var token = _service.GenerateToken(user);

            // Assert
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public void GenerateToken_WhenSecretIsMissing_ShouldReturnEmptyStringAndNotify()
        {
            // Arrange
            var user = new IdentityUser();

            _configuration.Setup(c => c["JwtSecurity:Secret"]).Returns((string)null);

            // Act
            var token = _service.GenerateToken(user);

            // Assert
            Assert.True(string.IsNullOrEmpty(token));
            Assert.Contains(_notificator.GetNotifications(), n => n.Message == "Error generating token");
        }

        [Fact]
        public void GenerateToken_WhenExceptionOccurs_ShouldReturnEmptyStringAndNotify()
        {
            // Arrange
            var user = new IdentityUser();
            _configuration.Setup(c => c["JwtSecurity:Secret"]).Throws(new Exception("Configuration error"));

            // Act
            var token = _service.GenerateToken(user);

            // Assert
            Assert.True(string.IsNullOrEmpty(token));
            Assert.Contains(_notificator.GetNotifications(), n => n.Message == "Error generating token");
        }

    }
}