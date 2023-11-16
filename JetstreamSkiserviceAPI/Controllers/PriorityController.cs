using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace JetstreamSkiserviceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityService _priorityService;
        private readonly ILogger<PriorityController> _logger;

        public PriorityController(IPriorityService priorityService, ILogger<PriorityController> logger)
        {
            _priorityService = priorityService;
            _logger = logger;
        }

        [HttpGet]
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

        [HttpGet("{priorityName}")]
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
