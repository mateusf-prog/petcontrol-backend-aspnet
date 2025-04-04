using FluentAssertions;
using IntegratedTests.Seed;
using IntegratedTests.Utils;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace IntegratedTests.Api.Controllers
{
    public class ProductControllerTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private const string ProductsApiUrl = "/api/products";

        public ProductControllerTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.HttpClient;
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenProductExists()
        {
            // Act
            var product = FakerData.GetValidFakerProduct();
            await _factory.Seed(product);

            // Act
            var response = await _client.GetAsync(ProductsApiUrl);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().Contain(product.Name);
            content.Should().Contain(product.Price.ToString());
            content.Should().Contain(product.Stock.ToString());
        }

        [Fact]
        public async Task GetById_ShoudReturnOk_WhenProductExists()
        {
            // Act
            var product = FakerData.GetValidFakerProduct();
            await _factory.Seed(product);
            var url = $"{ProductsApiUrl}/{product.Id}";            

            // Act
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().Contain(product.Name);
            content.Should().Contain(product.Price.ToString());
            content.Should().Contain(product.Stock.ToString());
        }

        [Fact]
        public async Task GetById_ShoudReturn404_WhenProductNotExists()
        {
            // Act
            var url = $"{ProductsApiUrl}/{Guid.NewGuid()}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_ShouldReturnCreated_WhenProductIsValid()
        {
            // Arrange
            var fakerProduct = FakerData.GetValidFakerProduct();
            var content = JsonContent.Create(fakerProduct);

            // Act
            var response = await _client.PostAsync(ProductsApiUrl, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenProductIsInvalid()
        {
            // Arrange
            var invalidProduct = FakerData.GetValidFakerProduct();
            invalidProduct.Update("", 0, 0, ""); 
            var content = JsonContent.Create(invalidProduct);

            // Act
            var response = await _client.PostAsync(ProductsApiUrl, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenProductIsValid()
        {
            // Arrange
            var product = FakerData.GetValidFakerProduct();
            await _factory.Seed(product);
            var updatedProduct = FakerData.GetValidFakerProduct();
            updatedProduct.Id = product.Id;
            var url = $"{ProductsApiUrl}/{product.Id}";
            var content = JsonContent.Create(updatedProduct);

            // Act
            var response = await _client.PutAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenProductNotExists()
        {
            // Arrange
            var product = FakerData.GetValidFakerProduct();
            var url = $"{ProductsApiUrl}/{Guid.NewGuid()}";
            var content = JsonContent.Create(product);

            // Act
            var response = await _client.PutAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Should().Contain("Product not found");
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenProductIsInvalid()
        {
            // Arrange
            var product = FakerData.GetValidFakerProduct();
            await _factory.Seed(product);
            var updatedProduct = FakerData.GetValidFakerProduct();
            updatedProduct.Id = product.Id;
            updatedProduct.Update("", 0, 0, "");
            var url = $"{ProductsApiUrl}/{product.Id}";
            var content = JsonContent.Create(updatedProduct);

            // Act
            var response = await _client.PutAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenProductExists()
        {
            // Arrange
            var product = FakerData.GetValidFakerProduct();
            await _factory.Seed(product);
            var url = $"{ProductsApiUrl}/{product.Id}";

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenProductNotExists()
        {
            // Arrange
            var url = $"{ProductsApiUrl}/{Guid.NewGuid()}";

            // Act
            var response = await _client.DeleteAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Should().Contain("Product not found");
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
