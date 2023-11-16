namespace JetstreamSkiserviceAPI.DTO
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Attempts { get; set; }
    }
}
