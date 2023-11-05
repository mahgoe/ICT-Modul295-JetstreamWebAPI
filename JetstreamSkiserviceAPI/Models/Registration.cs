using System.ComponentModel.DataAnnotations;

namespace JetstreamSkiserviceAPI.Models
{
    /// <summary>
    /// Priority Class to create Database (Code first) - Database connection
    /// It contains all the necessary information related to a registration
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Unique identifier for the registration
        /// </summary>
        [Key]
        public int RegistrationId { get; set; }

        /// <summary>
        /// Name of the person registering for the ski service (FirstName + LastName)
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Email address of the person registering for the ski service
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the person registering for the ski service
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Phone {  get; set; }

        /// <summary>
        /// The date and time when the registration was created/ Service begin date and time
        /// </summary>
        [Required]
        public DateTime Create_date { get; set; }

        /// <summary>
        /// The date and time when the skis should be picked up after service
        /// </summary>
        [Required]
        public DateTime Pickup_date { get; set; }

        /// <summary>
        /// Current status of the registration (Offen, InArbeit, abgeschlossen)
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Priority level of the service request (Tief, Standard, Express)
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Type of service requested from the client
        /// </summary>
        public Service Service { get; set; }

        /// <summary>
        /// Price of the service
        /// </summary>
        public string? Price { get; set; }

        /// <summary>
        /// Additional comments or special instructions for the ski service
        /// </summary>
        [StringLength(255)]
        public string? Comment { get; set; }
    }
}
