using FluentAssertions;
using IntegratedTests.Seed;
using IntegratedTests.Utils;
using PetControlSystem.Api.Mappers;
using PetControlSystem.Domain.Entities;
using System.Net;
using System.Net.Http.Json;

namespace IntegratedTests.Api.Controllers;

public class CustomerControllerTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private const string CustomersApiUrl = "/api/customers";

    public CustomerControllerTest(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.HttpClient;
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WhenCustomerExists()
    {
        // Arrange
        var customer = FakerData.GetValidFakerCustomer();
        await _factory.Seed(customer);

        // Act
        var response = await _client.GetAsync(CustomersApiUrl);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().Contain(customer.Name);
        content.Should().Contain(customer.Email);
        content.Should().Contain(customer.Phone);
        content.Should().Contain(customer.Document);
        content.Should().Contain(customer.Email);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenCustomerExists()
    {
        // Arrange
        var customer = new Customer("name-test", "teste@email.com", "12345678900", "12345678900", null);
        await _factory.Seed(customer);
        var url = $"{CustomersApiUrl}/{customer.Id}";

        // Act
        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().Contain(customer.Name);
        content.Should().Contain(customer.Email);
        content.Should().Contain(customer.Phone);
        content.Should().Contain(customer.Document);
        content.Should().Contain(customer.Email);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        var url = $"{CustomersApiUrl}/1";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_ShouldReturnOk_WhenCustomerIsCreated()
    {
        // Arrange
        var input = FakerData.GetValidFakerCustomer().ToDto();
        var content = JsonContent.Create(input);
        var url = CustomersApiUrl;

        // Act
        var response = await _client.PostAsync(url, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenInputIsInvalid()
    {
        // Arrange
        var address = FakerData.GetValidFakerAddress();
        var input = new Customer("name", "invalid-email", "12890230942", "12890230942", address);
        var content = JsonContent.Create(input);
        var url = CustomersApiUrl;

        // Act
        var response = await _client.PostAsync(url, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldReturn204NoContent_WhenCustomerIsValidAndExistsOnDatabase()
    {
        // Arrange
        var customerExistent = FakerData.GetValidFakerCustomer();
        await _factory.Seed(customerExistent);
        var address = FakerData.GetValidFakerAddress();
        customerExistent.Update("name-updated", "email@updated.com", "00000000000", "92939495821", address);
        var content = JsonContent.Create(customerExistent.ToDto());

        var url = $"{CustomersApiUrl}/{customerExistent.Id}";

        // Act
        var response = await _client.PutAsync(url, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Put_ShouldReturnBadRequest_WhenInputIsInvalid()
    {
        // Arrange
        var customerExistent = FakerData.GetValidFakerCustomer();
        var address = FakerData.GetValidFakerAddress();
        customerExistent.Update("name-updated", "invalid-email-format", "00000000000", "92939495821", address);
        var content = JsonContent.Create(customerExistent.ToDto());

        var url = $"{CustomersApiUrl}/{customerExistent.Id}";

        // Act
        var response = await _client.PutAsync(url, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenCustomerIsDeleted()
    {
        // Arrange
        var customerExistent = FakerData.GetValidFakerCustomer();
        await _factory.Seed(customerExistent);

        var url = $"{CustomersApiUrl}/{customerExistent.Id}";

        // Act
        var response = await _client.DeleteAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnBadRequestAndNotification_WhenCustomerIsNotFound()
    {
        // Arrange
        var url = $"{CustomersApiUrl}/{Guid.NewGuid()}";

        // Act
        var response = await _client.DeleteAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
