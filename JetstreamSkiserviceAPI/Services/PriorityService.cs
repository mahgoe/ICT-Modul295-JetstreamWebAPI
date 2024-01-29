using AutoMapper;
using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JetstreamSkiserviceAPI.Services
{
    /// <summary>
    /// The PriorityService class provides methods to interact with priority data in the database, implementing from IPriorityService
    /// </summary>
    public class PriorityService : IPriorityService
    {
        private readonly RegistrationsContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the PriorityService class
        /// </summary>
        /// <param name="context">The database context used for data operations</param>
        public PriorityService(RegistrationsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all priorities along with their associated registrations from the database
        /// </summary>
        /// <returns>A collection of registrations ordered by priorities</returns>
        public async Task<IEnumerable<PriorityDto>> GetAll()
        {
            var priorities = await _context.Priority
                .Include(p => p.Registrations)
                    .ThenInclude(r => r.Status)
                .Include(p => p.Registrations)
                    .ThenInclude(r => r.Service)
                .ToListAsync();

            return priorities.Select(s => new PriorityDto
            {
                PriorityId = s.PriorityId,
                PriorityName = s.PriorityName,
                Registration = _mapper.Map<List<RegistrationDto>>(s.Registrations)
            }).ToList();
        }

        /// <summary>
        /// Retrieves a single priority by its name along with its associated registrations
        /// </summary>
        /// <param name="priorityName">The name of the priority to retrieve (Tief/Standard/Express)</param>
        /// <returns>A collection of registrations with the associated priority</returns>
        public async Task<PriorityDto> GetByPriority(string priorityName)
        {
            var priority = await _context.Priority
                .Include(p => p.Registrations)
                    .ThenInclude(r => r.Status)
                .Include(p => p.Registrations) 
                    .ThenInclude(r => r.Service)
            .FirstOrDefaultAsync(p => p.PriorityName == priorityName);

            if (priority != null)
            {
                var priorityDto = new PriorityDto
                {
                    PriorityId = priority.PriorityId,
                    PriorityName = priority.PriorityName,
                    Registration = priority.Registrations.Select(r => new RegistrationDto
                    {
                        RegistrationId = r.RegistrationId,
                        FirstName = r.FirstName,
                        LastName = r.LastName,
                        Email = r.Email,
                        Phone = r.Phone,
                        Create_date = r.Create_date,
                        Pickup_date = r.Pickup_date,
                        Priority = r.Priority.PriorityName,
                        Service = r.Service.ServiceName,
                        Status = r.Status.StatusName,
                        Price = r.Price,
                        Comment = r.Comment
                    }).ToList()
                };
                return priorityDto;
            }
            return null;
        }
    }
}
