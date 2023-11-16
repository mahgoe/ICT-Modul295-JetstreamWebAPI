using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JetstreamSkiserviceAPI.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace JetstreamSkiserviceAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ITokenService tokenService, ILogger<EmployeesController> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        public List<Employee> Employee { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] AuthDto employee)
        {
            try
            {
                var employees = _tokenService.GetEmployees();

                foreach (Employee user in employees)
                {
                    if (employee.Username == user.Username)
                    {
                        if (employee.Password == user.Password)
                        {
                            if (user.Attempts >= 3)
                            {
                                return Unauthorized("Your account is locked. Please contact administrator.");
                            }
                            else
                            {
                                _tokenService.Unban(user.EmployeeId);
                                return Ok(new { username = user.Username, token = _tokenService.CreateToken(user.Username) });
                            }
                        }
                        else
                        {
                            _tokenService.Attempts(user.EmployeeId);
                            if (user.Attempts >= 3)
                            {
                                return Unauthorized("Your account is locked. Please contact administrator.");
                            }
                            return Unauthorized($"Invalid password!! {3 - user.Attempts} attempts left.");
                        }
                    }
                }

                return Unauthorized("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("unban/{id}")]
        public ActionResult Unban(int id) 
        {
            try
            {
                _tokenService.Unban(id);
                return Ok("Account unbanned");
            } catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}