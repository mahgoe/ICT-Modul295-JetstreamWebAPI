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
    /// xUnit Tests for PriorityController
    /// </summary>
    public class PriorityControllerTests
    {
        private readonly Mock<IPriorityService> _mockPriorityService = new Mock<IPriorityService>();
        private readonly Mock<ILogger<PriorityController>> _mockLogger = new Mock<ILogger<PriorityController>>();

        [Fact]
        public async Task GetAll_ReturnsAllPriorities()
        {
            // Arrange
            var priorityList = new List<PriorityDto>
        {
            new PriorityDto { PriorityName = "Tief" },
            new PriorityDto { PriorityName = "Standard" },
            new PriorityDto { PriorityName = "Express" }
        };

            _mockPriorityService.Setup(s => s.GetAll()).ReturnsAsync(priorityList);
            var controller = new PriorityController(_mockPriorityService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(priorityList);
        }

        [Fact]
        public async Task GetByPriority_ExistingPriority_ReturnsPriority()
        {
            // Arrange
            var priorityDto = new PriorityDto { PriorityName = "Standard" };
            _mockPriorityService.Setup(s => s.GetByPriority("Standard")).ReturnsAsync(priorityDto);
            var controller = new PriorityController(_mockPriorityService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetByPriority("Standard");

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(priorityDto);
        }

        [Fact]
        public async Task GetByPriority_NonExistingPriority_ReturnsNotFound()
        {
            // Arrange
            _mockPriorityService.Setup(s => s.GetByPriority("NonExisting")).ReturnsAsync((PriorityDto)null);
            var controller = new PriorityController(_mockPriorityService.Object, _mockLogger.Object);

            // Act
            var result = await controller.GetByPriority("NonExisting");

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}