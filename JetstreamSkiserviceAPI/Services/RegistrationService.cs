using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JetstreamSkiserviceAPI.Services
{
    /// <summary>
    /// The RegistrationService class provides methods to get and set data from the heart of the database, implementing from IRegistrationService
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        private readonly RegistrationsContext _context;

        /// <summary>
        /// Initializes a new instance of the RegistrationService class with the specified database context
        /// </summary>
        /// <param name="context">The database context to be used for data operations</param>
        public RegistrationService(RegistrationsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all registrations from the database and returns them as a list of RegistrationDto objects
        /// </summary>
        public async Task<IEnumerable<RegistrationDto>> GetRegistrations()
        {
            return await _context.Registrations
                .Select(Registration => new RegistrationDto
                {
                    RegistrationId = Registration.RegistrationId,
                    FirstName = Registration.FirstName,
                    LastName = Registration.LastName,
                    Email = Registration.Email,
                    Phone = Registration.Phone,
                    Create_date = Registration.Create_date,
                    Pickup_date = Registration.Pickup_date,
                    Status = Registration.Status.StatusName,
                    Priority = Registration.Priority.PriorityName,
                    Service = Registration.Service.ServiceName,
                    Price = Registration.Price,
                    Comment = Registration.Comment
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a single registration by its ID and returns it as a RegistrationDto object.
        /// </summary>
        /// <param name="id">The ID of the registration to retrieve.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Referenced ID or Item not found or doesn't exist</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during the process</exception>
        public async Task<RegistrationDto> GetRegistrationById(int id)
        {
            var registration = await _context.Registrations
                .Include(r => r.Status)
                .Include(r => r.Priority)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(r => r.RegistrationId == id);

            if (registration == null)
            {
                throw new KeyNotFoundException("Referenced ID or Item not found or doesn't exist");
            }

            try
            {
                return new RegistrationDto
                {
                    RegistrationId = registration.RegistrationId,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    Email = registration.Email,
                    Phone = registration.Phone,
                    Create_date = registration.Create_date,
                    Pickup_date = registration.Pickup_date,
                    Status = registration.Status?.StatusName,
                    Priority = registration.Priority?.PriorityName,
                    Service = registration.Service?.ServiceName,
                    Price = registration.Price,
                    Comment = registration.Comment
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// Adds a new registration to the database based on the provided RegistrationDto object
        /// </summary>
        /// <param name="registrationDto">The RegistrationDto object containing the registration details</param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during the process</exception>
        public async Task<RegistrationDto> AddRegistration(RegistrationDto registrationDto)
        {
            var registration = new Registration
            {
                RegistrationId = registrationDto.RegistrationId,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                Phone = registrationDto.Phone,
                Create_date = registrationDto.Create_date,
                Pickup_date = registrationDto.Pickup_date,
                Status = _context.Status.FirstOrDefault(e => e.StatusName == registrationDto.Status),
                Priority = _context.Priority.FirstOrDefault(e => e.PriorityName == registrationDto.Priority),
                Service = _context.Services.FirstOrDefault(e => e.ServiceName == registrationDto.Service),
                Price = registrationDto.Price,
                Comment = registrationDto.Comment
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
            registrationDto.RegistrationId = registration.RegistrationId;

            return registrationDto;
        }

        /// <summary>
        /// Updates an existing registration in the database based on the provided RegistrationDto object.
        /// </summary>
        /// <param name="registrationDto">The RegistrationDto object containing the updated registration details</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Referenced ID or Item not found or doesn't exist</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during the process</exception>
        public async Task UpdateRegistration(RegistrationDto registrationDto)
        {
            var registration = await _context.Registrations.FindAsync(registrationDto.RegistrationId);
            if (registration == null)
            {
                throw new KeyNotFoundException("Referenced ID or Item not found or doesn't exist");
            }

            registration.FirstName = registration.FirstName;
            registration.LastName = registration.LastName;
            registration.Email = registrationDto.Email;
            registration.Phone = registrationDto.Phone;
            registration.Create_date = registrationDto.Create_date;
            registration.Pickup_date = registrationDto.Pickup_date;
            registration.Status = _context.Status.FirstOrDefault(e => e.StatusName == registrationDto.Status);
            registration.Priority = _context.Priority.FirstOrDefault(e => e.PriorityName == registrationDto.Priority);
            registration.Service = _context.Services.FirstOrDefault(e => e.ServiceName == registrationDto.Service);
            registration.Price = registrationDto.Price;
            registration.Comment = registrationDto.Comment;

            _context.Entry(registration).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a registration from the database by its ID
        /// </summary>
        /// <param name="id">The ID of the registration to delete</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Referenced ID or Item not found or doesn't exist</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during the process</exception>
        public async Task DeleteRegistration(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                throw new KeyNotFoundException("Referenced ID or Item not found or doesn't exist");
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
        }
    }
}
