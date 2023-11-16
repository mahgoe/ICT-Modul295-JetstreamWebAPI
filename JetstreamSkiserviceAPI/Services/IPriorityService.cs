using JetstreamSkiserviceAPI.DTO;

namespace JetstreamSkiserviceAPI.Services
{
    public interface IPriorityService
    {
        Task<IEnumerable<PriorityDto>> GetAll();
        Task<PriorityDto> GetByPriority(string priorityName);
    }
}
