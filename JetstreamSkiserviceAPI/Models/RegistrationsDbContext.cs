// Add-Migration "Initial Create"
// Update-Database

using Microsoft.EntityFrameworkCore;

namespace JetstreamSkiserviceAPI.Models
{
    public class RegistrationsContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public RegistrationsContext()
        {
        }

        public RegistrationsContext(DbContextOptions<RegistrationsContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string config = _configuration.GetConnectionString("JetstreamSkiserviceDB");
            optionsBuilder.UseSqlServer($@"{config}");
        }
    }
}
