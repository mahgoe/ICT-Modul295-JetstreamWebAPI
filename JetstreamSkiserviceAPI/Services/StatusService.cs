using AutoMapper;
using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JetstreamSkiserviceAPI.Services
{
    /// <summary>
    /// The StatusService class provides methods to interact with status data in the database, implementing from IStatusService
    /// </summary>
    public class StatusService : IStatusService
    {
        private readonly RegistrationsContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the StatusService class
        /// </summary>
        /// <param name="context">The database context used for data operations</param>
        public StatusService(RegistrationsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all statuses along with their associated registrations from the database
        /// </summary>
        /// <returns>A collection of registrations ordered by status</returns>
        public async Task<IEnumerable<StatusDto>> GetAll()
        {
            var statuses = await _context.Status
                .Include(s => s.Registrations)
                    .ThenInclude(r => r.Priority)
                .Include(s => s.Registrations)
                    .ThenInclude(r => r.Service)
                .ToListAsync();

            return statuses.Select(s => new StatusDto
            {
                StatusId = s.StatusId,
                StatusName = s.StatusName,
                Registration = _mapper.Map<List<RegistrationDto>>(s.Registrations)
            }).ToList();
        }


        /// <summary>
        /// Retrieves a single status by its name along with its associated registrations
        /// </summary>
        /// <param name="statusName">The name of the status to retrieve (Offen/InArbeit/abgeschlossen)</param>
        /// <returns>A collection of Registrations with the associated status</returns>
        public async Task<StatusDto> GetByStatus(string statusName)
        {
            var status = await _context.Status
                .Include(s => s.Registrations)
                    .ThenInclude(r => r.Priority)
                .Include(s => s.Registrations)
                    .ThenInclude(r => r.Service)
            .FirstOrDefaultAsync(s => s.StatusName == statusName);

            if (status != null)
            {
                var statusDto = new StatusDto
                {
                    StatusId = status.StatusId,
                    StatusName = status.StatusName,
                    Registration = status.Registrations.Select(r => new RegistrationDto
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
                return statusDto;
            }
            return null;
        }
    }
}
