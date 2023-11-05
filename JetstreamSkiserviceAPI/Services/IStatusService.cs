using JetstreamSkiserviceAPI.DTO;

namespace JetstreamSkiserviceAPI.Services
{
    /// <summary>
    /// Interface for Status
    /// </summary>
    /// <remarks>
    ///  To get Registrations ordered by Status (Offen/InArbeit/abgeschlossen)
    /// </remarks>
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAll();

        Task<StatusDto> GetByStatus(string statusName);
    }
}
