using Moq;
using Microsoft.Extensions.Logging;
using JetstreamSkiserviceAPI.Controllers;
using JetstreamSkiserviceAPI.Services;
using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;


namespace JetstreamSkiserviceAPI_Test.ControllerTests
{
    /// <summary>
    /// xUnit Tests for EmployeeController
    /// </summary>
    public class EmployeeControllerTests
    {
        private readonly Mock<ITokenService> _mockTokenService = new Mock<ITokenService>();
        private readonly Mock<ILogger<EmployeesController>> _mockLogger = new Mock<ILogger<EmployeesController>>();

        [Fact]
        public void Login_ValidCredentials_ReturnsOkResultWithToken()
        {
            // Arrange
            var employee = new AuthDto { Username = "tuser", Password = "tpassword" };
            var controller = new EmployeesController(_mockTokenService.Object, _mockLogger.Object);
            _mockTokenService.Setup(s => s.GetEmployees()).Returns(new List<Employee>
            {
                new Employee { Username = "tuser", Password = "tpassword", Attempts = 0 }
            });
            _mockTokenService.Setup(s => s.CreateToken(It.IsAny<string>())).Returns("ttoken");

            // Act
            var result = controller.Login(employee);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { username = "tuser", token = "ttoken" });
        }

        [Fact]
        public void Login_ThreeFailedAttempts_BansUser()
        {
            // Arrange
            var employee = new AuthDto { Username = "tuser", Password = "wrongpassword" };
            var controller = new EmployeesController(_mockTokenService.Object, _mockLogger.Object);

            var testEmployee = new Employee { Username = "tuser", Password = "correctpassword", Attempts = 0 };
            var employees = new List<Employee> { testEmployee };

            _mockTokenService.Setup(s => s.GetEmployees()).Returns(employees);
            _mockTokenService.Setup(s => s.Attempts(It.IsAny<int>()))
                             .Callback((int id) => { testEmployee.Attempts++; });

            // Act and Assert
            // 1st Attempt
            var result1 = controller.Login(employee);
            result1.Should().BeOfType<UnauthorizedObjectResult>();
            testEmployee.Attempts.Should().Be(1);

            // 2nd Attempt
            var result2 = controller.Login(employee);
            result2.Should().BeOfType<UnauthorizedObjectResult>();
            testEmployee.Attempts.Should().Be(2);

            // 3rd Attempt
            var result3 = controller.Login(employee);
            result3.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result3 as UnauthorizedObjectResult;
            unauthorizedResult.Value.Should().Be("Your account is locked. Please contact administrator.");
            testEmployee.Attempts.Should().Be(3);
        }

        [Fact]
        public void Unban_ValidId_ReturnsOkResult()
        {
            // Arrange
            var controller = new EmployeesController(_mockTokenService.Object, _mockLogger.Object);
            int validId = 1;
            _mockTokenService.Setup(s => s.Unban(validId)).Verifiable();

            // Act
            var result = controller.Unban(validId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _mockTokenService.Verify();
        }
    }
}

