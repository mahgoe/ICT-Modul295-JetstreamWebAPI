using Moq;
using Microsoft.Extensions.Logging;
using JetstreamSkiserviceAPI.Controllers;
using JetstreamSkiserviceAPI.Services;
using JetstreamSkiserviceAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace JetstreamSkiserviceAPI_Test.ControllerTests
{
    /// <summary>
    /// xUnit Tests for StatusController
    /// </summary>
    public class StatusControllerTests
    {
        private readonly Mock<IStatusService> _mockStatusService = new Mock<IStatusService>();
        private readonly Mock<ILogger<StatusController>> _mockLogger = new Mock<ILogger<StatusController>>();

        [Fact]
        public async Task GetAll_ReturnsAllStatuses()
        {
            // Arrange
            var statusList = new List<StatusDto> 
            { 
                new StatusDto
                {
                    StatusId = 1,
                    StatusName = "Offen"
                },
                new StatusDto 
                { 
                    StatusId = 2,
                    StatusName = "InArbeit"
                },
                new StatusDto
                {
                    StatusId = 3,
                    StatusName = "abgeschlossen"
                }
            };
            _mockStatusService.Setup(s => s.GetAll()).ReturnsAsync(statusList);

            var controller = new StatusController(_mockStatusService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(statusList);
        }

        [Fact]
        public async Task GetByStatus_ExistingStatusName_ReturnsStatus()
        {
            // Arrange
            var statusDto = new StatusDto 
            {
                StatusId = 1,
                StatusName = "Offen",

                Registration = new List<RegistrationDto>
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
                    }
                }
            };
            _mockStatusService.Setup(s => s.GetByStatus("Offen")).ReturnsAsync(statusDto);
            var controller = new StatusController(_mockStatusService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetByStatus("Offen");

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(statusDto);
        }

        [Fact]
        public async Task GetByStatus_NonExistingStatusName_ReturnsNotFound()
        {
            // Arrange
            _mockStatusService.Setup(s => s.GetByStatus("Unbekannt")).ReturnsAsync((StatusDto)null);
            var controller = new StatusController(_mockStatusService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetByStatus("Unbekannt");

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
