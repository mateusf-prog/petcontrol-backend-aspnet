using FluentAssertions;
using IntegratedTests.Utils;
using PetControlSystem.Api.Dto;
using System.Net;
using System.Net.Http.Json;

namespace IntegratedTests.Api.Controllers
{
    [Collection("IntegratedTests")]
    public class AuthControllerTest : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private const string CustomersApiUrl = "/api/auth";
        private const string EmailLogin = "testuser@example.com";
        private const string PasswordLogin = "Teste@Password123";

        public AuthControllerTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.HttpClient;
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenUserExists()
        {
            // Arrange
            var user = AuthConfig.GetValidUser();
            var url = $"{CustomersApiUrl}/login";
            await _factory.Seed(user);
            var input = new UserLoginDto
            {
                Email = EmailLogin,
                Password = PasswordLogin
            };
            var content = JsonContent.Create(input);

            // Act
            var response = await _client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenUserPasswordIncorrect()
        {
            // Arrange
            var user = AuthConfig.GetValidUser();
            var url = $"{CustomersApiUrl}/login";
            await _factory.Seed(user);
            var input = new UserLoginDto
            {
                Email = EmailLogin,
                Password = "any-password"
            };
            var content = JsonContent.Create(input);

            // Act
            var response = await _client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Should().Contain("Incorrect password.");
        }

        [Fact]
        public async Task Register_Should200Ok_WhenValidData()
        {
            // Arrange
            var input = AuthConfig.GetValidUserDto();
            var url = $"{CustomersApiUrl}/register";
            var content = JsonContent.Create(input);

            // Act
            var response = await _client.PostAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidData()
        {
            // Arrange
            var input = AuthConfig.GetInvalidUserDto();
            var url = $"{CustomersApiUrl}/register";
            var content = JsonContent.Create(input);

            // Act
            var response = await _client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Should().Contain("The field Email is in an invalid format");
        }

        public async Task InitializeAsync()
        {
            await _factory.ResetDatabaseAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
