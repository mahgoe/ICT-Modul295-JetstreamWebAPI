using System.ComponentModel.DataAnnotations;

namespace JetstreamSkiserviceAPI.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public int Attempts { get; set; }
    }
}
