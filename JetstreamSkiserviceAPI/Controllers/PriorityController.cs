using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JetstreamSkiserviceAPI.Controllers
{
    /// <summary>
    /// Controller for handling HTTP requests related to Priority/order by Priorities
    /// </summary>
    /// /// <remarks>
    /// Use these Priorities (Tief/Standard/Express)
    /// </remarks>
    [ApiController]
    [Route("[controller]")]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityService _priorityService;
        private readonly ILogger<PriorityController> _logger;

        /// <summary>
        /// Constructor for PriorityController
        /// </summary>
        /// <param name="priorityService">Implementation of the IPriorityService interface</param>
        /// <param name="logger">Logger for logging information and errors</param>
        public PriorityController(IPriorityService priorityService, ILogger<PriorityController> logger)
        {
            _priorityService = priorityService;
            _logger = logger;
        }

        /// <summary>
        /// Handles GET requests to retrieve all priorities entities
        /// </summary>
        /// <returns>A list sorted by priorities</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PriorityDto>>> GetAll()
        {
            try
            {
                return Ok(await _priorityService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError($"An Error occurred, {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        /// <summary>
        /// Handles GET requests to retrieve a specific priority entity by its name
        /// </summary>
        /// <param name="statusName">The name of the status to retrieve (Tief/Standard/Express)</param>
        /// <param name="priorityName">Registrations by the name of the priority to retrieve</param>
        /// <returns></returns>
        [HttpGet("{priorityName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PriorityDto>>> GetByPriority(string priorityName)
        {
            try
            {
                var priorityDto = await _priorityService.GetByPriority(priorityName);
                if(priorityDto == null)
                {
                    return NotFound();
                }
                return Ok(priorityDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An Error occurred, {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
    }
}
