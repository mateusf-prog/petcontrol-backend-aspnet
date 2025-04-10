using System.Net;
using IntegratedTests.Seed;
using IntegratedTests.Utils;
using FluentAssertions;
using PetControlSystem.Api.Dto;
using System.Net.Http.Json;

namespace IntegratedTests.Api.Controllers;

[Collection("IntegratedTests")]
public class OrdersControllerTest : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private const string OrdersApiUrl = "/api/orders";

    public OrdersControllerTest(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.HttpClient;
    }

    [Fact]
    public async Task GetAll_ReturnsSuccessStatusCode()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var order = FakerData.GetOrder(customer.Id);
        await _factory.Seed(order);

        // Act
        var response = await _client.GetAsync(OrdersApiUrl);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetById_ReturnsOrder_WhenOrderExists()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var order = FakerData.GetOrder(customer.Id);
        await _factory.Seed(order);
        var url = $"{OrdersApiUrl}/{order.Id}";

        // Act
        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode();
        content.Should().Contain(order.Id.ToString());
        content.Should().Contain(order.CustomerId.ToString());
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        // Act
        var response = await _client.GetAsync($"{OrdersApiUrl}/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_ReturnsCreatedOrder_WhenDataIsValid()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var product = FakerData.GetProduct();
        await _factory.Seed(product);

        var orderProducts = new List<OrderProductDto>
        {
            new()
            {
                ProductId = product.Id,
                Quantity = 2,
                Price = product.Price
            }
        };

        var orderDto = new OrderDto
        {
            CustomerId = customer.Id,
            Products = orderProducts,
            TotalPrice = product.Price * 2
        };

        var content = JsonContent.Create(orderDto);

        // Act
        var response = await _client.PostAsync(OrdersApiUrl, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenDataIsInvalid()
    {
        // Arrange
        var orderDto = new OrderDto
        {
            CustomerId = Guid.NewGuid(),
            TotalPrice = 1500.0m
        };
        var content = JsonContent.Create(orderDto);

        // Act
        var response = await _client.PostAsync(OrdersApiUrl, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenOrderExists()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var order = FakerData.GetOrder(customer.Id);
        await _factory.Seed(order);
        var product = FakerData.GetProduct();
        await _factory.Seed(product);
        var orderProducts = new List<OrderProductDto>
        {
            new()
            {
                ProductId = product.Id,
                Quantity = 2,
                Price = product.Price
            }
        };
        var orderDto = new OrderDto
        {
            Id = order.Id,
            CustomerId = customer.Id,
            Products = orderProducts,
            TotalPrice = 2000.0m
        };

        var url = $"{OrdersApiUrl}/{order.Id}";
        var content = JsonContent.Create(orderDto);

        // Act
        var response = await _client.PutAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenOrderNotExists()
    {
        // Arrange
        var orderProducts = new List<OrderProductDto>
        {
            new()
            {
                ProductId = Guid.NewGuid(),
                Quantity = 2,
                Price = 1500m
            }
        };
        var orderDto = new OrderDto
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Products = orderProducts,
            TotalPrice = 2000.0m
        };

        var url = $"{OrdersApiUrl}/{Guid.NewGuid()}";
        var content = JsonContent.Create(orderDto);

        // Act
        var response = await _client.PutAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Should().Contain("Order not found");
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenOrderExists()
    {
        // Arrange
        var customer = FakerData.GetCustomer();
        await _factory.Seed(customer);
        var order = FakerData.GetOrder(customer.Id);
        await _factory.Seed(order);
        var url = $"{OrdersApiUrl}/{order.Id}";

        // Act
        var response = await _client.DeleteAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenOrderNotExists()
    {
        // Act
        var response = await _client.DeleteAsync($"{OrdersApiUrl}/{Guid.NewGuid()}");
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Should().Contain("Order not found");
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