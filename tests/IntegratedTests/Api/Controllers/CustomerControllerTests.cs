using FluentAssertions;
using IntegratedTests.Utils;
using System.Net;

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
        await _factory.ResetDatabaseAsync();

        // Act
        var response = await _client.GetAsync(CustomersApiUrl);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenCustomerExists()
    {
        // Arrange
        var customerId = 1;
        var url = $"{CustomersApiUrl}/{customerId}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
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
