using FluentAssertions;
using IntegratedTests.Seed;
using IntegratedTests.Utils;
using PetControlSystem.Domain.Entities;
using System.Net;
using System.Net.Http.Json;

namespace IntegratedTests.Api.Controllers
{
    [Collection("IntegratedTests")]
    public class PetSupportControllerTests : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private const string PetSupportApiUrl = "/api/pet-supports";

        public PetSupportControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.HttpClient;
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenPetSupportExists()
        {
            // Arrange
            var petSupport = FakerData.GetPetSupport();
            await _factory.Seed(petSupport);

            // Act
            var response = await _client.GetAsync(PetSupportApiUrl);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            content.Should().Contain(petSupport.Id.ToString());
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenPetSupportExists()
        {
            // Arrange
            var petSupport = FakerData.GetPetSupport();
            await _factory.Seed(petSupport);
            var url = $"{PetSupportApiUrl}/{petSupport.Id}";

            // Act
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            content.Should().Contain(petSupport.Id.ToString());
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenPetSupportNotExists()
        {
            // Arrange
            var url = $"{PetSupportApiUrl}/-some-id";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_ShouldReturnCreated_WhenPetSupportIsValid()
        {
            // Arrange
            var petSupport = FakerData.GetPetSupport();
            var input = JsonContent.Create(petSupport);

            // Act
            var response = await _client.PostAsync(PetSupportApiUrl, input);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenPetSupportNotValid()
        {
            // Arrange
            var petSupport = new PetSupport("name", 0, 0, 0, []);
            var input = JsonContent.Create(petSupport);

            // Act
            var response = await _client.PostAsync(PetSupportApiUrl, input);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            content.Should().Contain("The field Name must be between 5 and 100 characters");
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenPetSupportExists()
        {
            // Arrange
            var petSupport = FakerData.GetPetSupport();
            await _factory.Seed(petSupport);
            var url = $"{PetSupportApiUrl}/{petSupport.Id}";
            petSupport.Update("name-updated", 10, 20, 30, []);
            var input = JsonContent.Create(petSupport);

            // Act
            var response = await _client.PutAsync(url, input);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenPetSupportNotExists()
        {
            // Arrange
            var petSupport = FakerData.GetPetSupport();
            var url = $"{PetSupportApiUrl}/-some-id";
            var input = JsonContent.Create(petSupport);

            // Act
            var response = await _client.PutAsync(url, input);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenPetSupportExists()
        {
            // Arrange
            var petSupport = FakerData.GetPetSupport();
            await _factory.Seed(petSupport);
            var url = $"{PetSupportApiUrl}/{petSupport.Id}";

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenPetSupportNotExists()
        {
            // Arrange
            var url = $"{PetSupportApiUrl}/-some-id";

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
