// Add-Migration "Initial Create"
// Update-Database

using Microsoft.EntityFrameworkCore;

namespace JetstreamSkiserviceAPI.Models
{
    /// <summary>
    /// Database context class representing the connection to the database
    /// It includes DbSet properties for the different entities and configurations for seeding initial data
    /// </summary>
    public class RegistrationsContext : DbContext
    {
        private readonly IConfiguration _configuration;

        // Parameterless constructor for enabling migrations
        public RegistrationsContext()
        {
        }

        // Constructor that takes options and configuration parameters
        public RegistrationsContext(DbContextOptions<RegistrationsContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        // DbSet properties for each entity
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Configures the model while building the database
        /// </summary>
        /// <param name="modelBuilder">Provides a simple API for configuring the model</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set standard data for Priority
            modelBuilder.Entity<Priority>().HasData(
                new Priority { PriorityId = 1, PriorityName = "Tief" },
                new Priority { PriorityId = 2, PriorityName = "Standard" },
                new Priority { PriorityId = 3, PriorityName = "Express" }
                );

            // Set standard data for Service
            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceId = 1, ServiceName = "Kleiner Service" },
                new Service { ServiceId = 2, ServiceName = "Grosser Service" },
                new Service { ServiceId = 3, ServiceName = "Rennski Service" },
                new Service { ServiceId = 4, ServiceName = "Bindungen montieren und einstellen" },
                new Service { ServiceId = 5, ServiceName = "Fell zuschneiden" },
                new Service { ServiceId = 6, ServiceName = "Heisswachsen" }
                );

            // Set standard data for Status
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = 1, StatusName = "Offen" },
                new Status { StatusId = 2, StatusName = "InArbeit" },
                new Status { StatusId = 3, StatusName = "abgeschlossen" },
                new Status { StatusId = 4, StatusName = "storniert" }
                );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Username = "admin", Password = "password", Attempts = 0 }
                );

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Configures the database to be used for this context
        /// This method is called for each instance of the context that is created
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string config = _configuration.GetConnectionString("JetstreamSkiserviceDB");
            optionsBuilder.UseSqlServer($@"{config}");
        }
    }
}
