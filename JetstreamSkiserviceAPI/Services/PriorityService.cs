using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JetstreamSkiserviceAPI.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly RegistrationsContext _context;

        public PriorityService(RegistrationsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PriorityDto>> GetAll()
        {
            var priorities = await _context.Priority
                .Include(p => p.Registrations)
                    .ThenInclude(r => r.Status)
                 .Include(p => p.Registrations)
                    .ThenInclude(r => r.Service)
                .ToListAsync();

            return priorities.Select(p => new PriorityDto
            {
                PriorityId = p.PriorityId,
                PriorityName = p.PriorityName,
                Registration = p.Registrations.Select(r => new RegistrationDto
                {
                    RegistrationId = r.RegistrationId,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Email = r.Email,
                    Phone = r.Phone,
                    Create_date = r.Create_date,
                    Pickup_date = r.Pickup_date,
                    Priority = p.PriorityName,
                    Service = r.Service.ServiceName,
                    Status = r.Status.StatusName,
                    Price = r.Price,
                    Comment = r.Comment
                }).ToList()
            }).ToList();
        }

        public async Task<PriorityDto> GetByPriority(string priorityName)
        {
            var allPriorities = await GetAll();
            return allPriorities.FirstOrDefault(p => p.PriorityName == priorityName);
        }
    }
}
