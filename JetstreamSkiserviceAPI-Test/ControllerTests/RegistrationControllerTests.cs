using Moq;
using Microsoft.Extensions.Logging;
using JetstreamSkiserviceAPI.Controllers;
using JetstreamSkiserviceAPI.Services;
using JetstreamSkiserviceAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;


namespace JetstreamSkiserviceAPI_Test.ControllerTests
{
    public class RegistrationsControllerTests
    {
        private readonly Mock<IRegistrationService> _mockRegistrationService = new Mock<IRegistrationService>();
        private readonly Mock<ILogger<RegistrationsController>> _mockLogger = new Mock<ILogger<RegistrationsController>>();

        [Fact]
        public async Task GetRegistrations_ReturnsAllRegistrations()
        {
            // Arrange
            var registrationList = new List<RegistrationDto>
                {
                    new RegistrationDto
                    {
                        RegistrationId = 1,
                        FirstName = "Tim",
                        LastName = "Timmermann",
                        Email = "tim.timmermann@muster.com",
                        Phone = "0123456789",
                        Create_date = new DateTime(2023, 1, 1),
                        Pickup_date = new DateTime(2023, 1, 10),
                        Status = "Offen",
                        Priority = "Standard",
                        Service = "Kleiner Service",
                        Price = "50.00",
                        Comment = "Bitte schnell bearbeiten"
                    },
                    new RegistrationDto
                    {
                        RegistrationId = 2,
                        FirstName = "Meier",
                        LastName = "Schmidt",
                        Email = "Meier.schmidt@muster.com",
                        Phone = "09876543221",
                        Create_date = new DateTime(2023, 1, 5),
                        Pickup_date = new DateTime(2023, 1, 15),
                        Status = "InArbeit",
                        Priority = "Express",
                        Service = "Grosser Service",
                        Price = "80.00",
                        Comment = "Extra Wachs"
                    },
                };

            _mockRegistrationService.Setup(s => s.GetRegistrations()).ReturnsAsync(registrationList);
            var controller = new RegistrationsController(_mockRegistrationService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetRegistrations();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(registrationList);
        }

        [Fact]
        public async Task GetRegistration_ExistingId_ReturnsRegistration()
        {
            // Arrange
            var registrationDto = new RegistrationDto
            {
                RegistrationId = 1,
                FirstName = "Tim",
                LastName = "Timmermann",
                Email = "tim.timmermann@muster.com",
                Phone = "0123456789",
                Create_date = new DateTime(2023, 1, 1),
                Pickup_date = new DateTime(2023, 1, 10),
                Status = "Offen",
                Priority = "Standard",
                Service = "Kleiner Service",
                Price = "50.00",
                Comment = "Bitte schnell bearbeiten"
            };
            _mockRegistrationService.Setup(s => s.GetRegistrationById(1)).ReturnsAsync(registrationDto);
            var controller = new RegistrationsController(_mockRegistrationService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetRegistration(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(registrationDto);
        }

        [Fact]
        public async Task GetRegistration_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockRegistrationService.Setup(s => s.GetRegistrationById(99)).ReturnsAsync((RegistrationDto)null);
            var controller = new RegistrationsController(_mockRegistrationService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetRegistration(99);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateRegistration_ValidData_ReturnCreatedResult()
        {
            // Arrange
            var newRegistrationDto = new RegistrationDto
            {
                RegistrationId = 1,
                FirstName = "Tim",
                LastName = "Timmermann",
                Email = "tim.timmermann@muster.com",
                Phone = "0123456789",
                Create_date = new DateTime(2023, 1, 1),
                Pickup_date = new DateTime(2023, 1, 10),
                Status = "Offen",
                Priority = "Standard",
                Service = "Kleiner Service",
                Price = "50.00",
                Comment = "Bitte schnell bearbeiten"
            };
            _mockRegistrationService.Setup(s => s.AddRegistration(It.IsAny<RegistrationDto>()))
                .ReturnsAsync(newRegistrationDto);

            var controller = new RegistrationsController(_mockRegistrationService.Object, _mockLogger.Object);

            // Act
            var result = await controller.CreateRegistration(newRegistrationDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(newRegistrationDto, createdAtActionResult.Value);
        }

        [Fact]
        public async Task UpdateRegistration_ValidData_ReturnsOkResult()
        {
            // Arrange
            var updatedRegistrationDto = new RegistrationDto { RegistrationId = 1, /* ... Set other properties ... */ };
            _mockRegistrationService.Setup(s => s.UpdateRegistration(It.IsAny<RegistrationDto>()))
                                    .Returns(Task.CompletedTask);

            var controller = new RegistrationsController(_mockRegistrationService.Object, _mockLogger.Object);

            // Act
            var result = await controller.UpdateRegistration(1, updatedRegistrationDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateRegistration_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockRegistrationService.Setup(s => s.UpdateRegistration(It.IsAny<RegistrationDto>()))
                                    .Throws(new KeyNotFoundException());

            var controller = new RegistrationsController(_mockRegistrationService.Object, _mockLogger.Object);

            // Act
            var result = await controller.UpdateRegistration(99, new RegistrationDto { /* ... Set properties ... */ });

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteRegistration_ExistingId_ReturnsNoContent()
        {
            // Arrange
            _mockRegistrationService.Setup(s => s.DeleteRegistration(1)).Returns(Task.CompletedTask);
            var controller = new RegistrationsController(_mockRegistrationService.Object, _mockLogger.Object);

            // Act
            var result = await controller.DeleteRegistration(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteRegistration_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockRegistrationService.Setup(s => s.DeleteRegistration(99)).Throws(new KeyNotFoundException());
            var controller = new RegistrationsController(_mockRegistrationService.Object, _mockLogger.Object);

            // Act
            var result = await controller.DeleteRegistration(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
