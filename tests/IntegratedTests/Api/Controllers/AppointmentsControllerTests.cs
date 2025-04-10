using FluentAssertions;
using IntegratedTests.Seed;
using IntegratedTests.Utils;
using PetControlSystem.Api.Dto;
using System.Net;
using System.Net.Http.Json;

namespace IntegratedTests.Api.Controllers
{
    [Collection("IntegratedTests")]
    public class AppointmentsControllerTests : IAsyncLifetime
    {
        public readonly CustomWebApplicationFactory _factory;
        public readonly HttpClient _client;
        private const string AppointmentsApiUrl = "/api/appointments";

        public AppointmentsControllerTests(CustomWebApplicationFactory factory)
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
            var pet = FakerData.GetPet(customer.Id);
            await _factory.Seed(pet);
            var appointment = FakerData.GetAppointment(customer.Id, pet.Id);
            await _factory.Seed(appointment);

            // Act
            var response = await _client.GetAsync(AppointmentsApiUrl);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            result.Should().Contain(appointment.Id.ToString());
            result.Should().Contain(appointment.CustomerId.ToString());
            result.Should().Contain(appointment.PetId.ToString());
        }

        [Fact]
        public async Task GetById_ReturnsAppointment_WhenAppointmentExists()
        {
            // Arrange
            var customer = FakerData.GetCustomer();
            await _factory.Seed(customer);
            var pet = FakerData.GetPet(customer.Id);
            await _factory.Seed(pet);
            var appointment = FakerData.GetAppointment(customer.Id, pet.Id);
            await _factory.Seed(appointment);
            var url = $"{AppointmentsApiUrl}/{appointment.Id}";

            // Act
            var response = await _client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            result.Should().Contain(appointment.Id.ToString());
            result.Should().Contain(appointment.CustomerId.ToString());
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            // Arrange
            var url = $"{AppointmentsApiUrl}/{Guid.NewGuid()}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAppointment()
        {
            // Arrange
            var customer = FakerData.GetCustomer();
            await _factory.Seed(customer);
            var pet = FakerData.GetPet(customer.Id);
            await _factory.Seed(pet);
            var petSupport = FakerData.GetPetSupport();
            await _factory.Seed(petSupport);
            var appointment = FakerData.GetAppointment(customer.Id, pet.Id);
            var petSupports = new List<AppointmentPetSupportDto>
            {
                new AppointmentPetSupportDto
                {
                    PetSupportId = petSupport.Id,
                    Price = 1500m
                }
            };
            var input = new AppointmentDto
            {
                Date = appointment.Date,
                Description = appointment.Description,
                CustomerId = appointment.CustomerId,
                PetId = appointment.PetId,
                PetSupports = petSupports,
                TotalPrice = 1500m
            };

            // Act
            var response = await _client.PostAsync(AppointmentsApiUrl, JsonContent.Create(input));
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenAppointmentNotValid()
        {
            // Arrange
            var customer = FakerData.GetCustomer();
            await _factory.Seed(customer);
            var pet = FakerData.GetPet(customer.Id);
            await _factory.Seed(pet);
            var appointment = FakerData.GetAppointment(customer.Id, pet.Id);
            var input = new AppointmentDto
            {
                Date = appointment.Date,
                Description = appointment.Description,
                CustomerId = appointment.CustomerId,
                PetId = appointment.PetId,
                PetSupports = null,
                TotalPrice = null
            };

            // Act
            var response = await _client.PostAsync(AppointmentsApiUrl, JsonContent.Create(input));
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Should().Contain("The field PetSupports is required");
            result.Should().Contain("The field TotalPrice is required");
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenAppointmentIsUpdated()
        {
            // Arrange
            var customer = FakerData.GetCustomer();
            await _factory.Seed(customer);
            var pet = FakerData.GetPet(customer.Id);
            await _factory.Seed(pet);
            var appointment = FakerData.GetAppointment(customer.Id, pet.Id);
            await _factory.Seed(appointment);            
            var petSupport = FakerData.GetPetSupport();
            await _factory.Seed(petSupport);
            var petSupports = new List<AppointmentPetSupportDto>
            {
                new AppointmentPetSupportDto
                {
                    PetSupportId = petSupport.Id,
                    Price = 1500m
                }
            };
            var input = new AppointmentDto
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Description = "description-updated",
                CustomerId = appointment.CustomerId,
                PetId = appointment.PetId,
                PetSupports = petSupports,
                TotalPrice = 1500m
            };
            var url = $"{AppointmentsApiUrl}/{input.Id}";

            // Act
            var response = await _client.PutAsync(url, JsonContent.Create(input));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            // Arrange
            var petSupport = FakerData.GetPetSupport();
            await _factory.Seed(petSupport);
            var petSupports = new List<AppointmentPetSupportDto>
            {
                new AppointmentPetSupportDto
                {
                    PetSupportId = petSupport.Id,
                    Price = 1500m
                }
            };
            var input = new AppointmentDto
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(1),
                Description = "description-updated",
                CustomerId = Guid.NewGuid(),
                PetId = Guid.NewGuid(),
                PetSupports = petSupports,
                TotalPrice = 1500m
            };
            var url = $"{AppointmentsApiUrl}/{input.Id}";

            // Act
            var response = await _client.PutAsync(url, JsonContent.Create(input));
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Should().Contain("Appointment not found");
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenAppointmentIsDeleted()
        {
            // Arrange
            var customer = FakerData.GetCustomer();
            await _factory.Seed(customer);
            var pet = FakerData.GetPet(customer.Id);
            await _factory.Seed(pet);
            var appointment = FakerData.GetAppointment(customer.Id, pet.Id);
            await _factory.Seed(appointment);
            var url = $"{AppointmentsApiUrl}/{appointment.Id}";

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            // Arrange
            var url = $"{AppointmentsApiUrl}/{Guid.NewGuid()}";

            // Act
            var response = await _client.DeleteAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Should().Contain("Appointment not found");
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
}
