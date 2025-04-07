using FluentAssertions;
using IntegratedTests.Seed;
using IntegratedTests.Utils;
using PetControlSystem.Domain.Entities;
using System.Drawing;
using System.Net;
using System.Net.Http.Json;

namespace IntegratedTests.Api.Controllers;

[Collection("IntegratedTests")]
public class PetsControllerTest : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private const string PetsApiUrl = "/api/pets";

    public PetsControllerTest(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.HttpClient;
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WhenPetExists()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var pet = new Pet("Dog", "Buddy", 3, customer.Id);
        await _factory.Seed(pet);

        // Act
        var response = await _client.GetAsync(PetsApiUrl);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenPetExists()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var pet = FakerData.GetPet(customer.Id);
        await _factory.Seed(pet);
        var url = $"{PetsApiUrl}/{pet.Id}";

        // Act
        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().Contain(pet.Name);
    }

    [Fact]
    public async Task GetById_ShouldReturNotFound_WhenPetNotExists()
    {
        // Arrange
        var url = $"{PetsApiUrl}/{Guid.NewGuid()}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_ShouldReturnCreated_WhenPetIsValid()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var pet = FakerData.GetPet(customer.Id);
        var content = JsonContent.Create(pet);

        // Act
        var response = await _client.PostAsync(PetsApiUrl, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenPetIsInvalid()
    {
        // Arrange
        var pet = new Pet("", "", 0, Guid.Empty);
        var content = JsonContent.Create(pet);

        // Act
        var response = await _client.PostAsync(PetsApiUrl, content);
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Should().Contain("One or more validation errors occurred.");
    }

    [Fact]
    public async Task Update_ShouldReturnOk_WhenPetIsValid()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var pet = FakerData.GetPet(customer.Id);
        await _factory.Seed(pet);
        var url = $"{PetsApiUrl}/{pet.Id}";
        pet.Update("UpdatedName", "UpdatedDescription", 5, customer.Id);
        var content = JsonContent.Create(pet);

        // Act
        var response = await _client.PutAsync(url, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Update_ShouldReturnBadRequest_WhenPetIsInvalid()
    {
        // Arrange
        var pet = new Pet("", "", 0, Guid.Empty);
        var content = JsonContent.Create(pet);
        var url = $"{PetsApiUrl}/{Guid.NewGuid()}";

        // Act
        var response = await _client.PutAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Should().Contain("One or more validation errors occurred.");
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenPetExists()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var pet = FakerData.GetPet(customer.Id);
        await _factory.Seed(pet);
        var url = $"{PetsApiUrl}/{pet.Id}";

        // Act
        var response = await _client.DeleteAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenPetNotExists()
    {
        // Arrange
        var url = $"{PetsApiUrl}/{Guid.NewGuid()}";

        // Act
        var response = await _client.DeleteAsync(url);
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Should().Contain("Pet not found");
    }

    public async Task InitializeAsync()
    {
        await _factory.ResetDatabaseAsync();
    }

    public async Task DisposeAsync()
    {
        await Task.CompletedTask;
    }
}
