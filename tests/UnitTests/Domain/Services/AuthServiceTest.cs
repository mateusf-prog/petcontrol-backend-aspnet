using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Services;
using UnitTests.Fakers;

namespace UnitTests.Domain.Services
{
    public class AuthServiceTest
    {
        private readonly AuthService _authService;
        private readonly Notificator _notificator;
        private readonly Mock<FakeUserManager> _userManager = new();
        private readonly Mock<FakeSignInManager> _signInManager = new();
        private readonly Mock<ITokenService> _tokenService = new();

        public AuthServiceTest()
        {
            _notificator = new Notificator();
            _authService = new AuthService(_signInManager.Object,
                                           _userManager.Object,
                                           _tokenService.Object,
                                           _notificator);
        }

        [Fact]
        public async Task Login_WhenValidUser_ShouldReturnToken()
        {
            // Arrange
            var user = IdentityUserFaker.GetIdentityUser();
            var email = "test@email.com";
            var password = "123456";
            
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(user);
            _signInManager.Setup(s => s.PasswordSignInAsync(user.UserName, password, false, true))
                          .ReturnsAsync(SignInResult.Success);

            // Act
            var result = await _authService.Login(email, password);

            // Assert
            Assert.Equal(_tokenService.Object.GenerateToken(user), result);
        }

        [Fact]
        public async Task Login_WhenUserNotFound_ShouldNotify()
        {
            // Arrange            
            var email = "test@email.com";
            var password = "123456";

            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync((IdentityUser?)null);

            // Act
            var result = await _authService.Login(email, password);

            // Assert
            Assert.Equal(result, string.Empty);
            Assert.Equal("User not found.", _notificator.GetNotifications().Select(n => n.Message).First());
        }

        [Fact]
        public async Task Login_WhenPasswordIncorrect_ShouldNotify()
        {
            // Arrange            
            var email = "test@email.com";
            var password = "123456";
            var user = IdentityUserFaker.GetIdentityUser();

            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(user);
            _signInManager.Setup(s => s.PasswordSignInAsync(user.UserName, password, false, true))
                          .ReturnsAsync(SignInResult.Failed);

            // Act
            var result = await _authService.Login(email, password);

            // Assert
            Assert.Equal(result, string.Empty);
            Assert.Equal("Incorrect password.", _notificator.GetNotifications().Select(n => n.Message).First());
        }
    }
}