namespace PMS.Api.Dtos
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string? Email { get; set; }
    }
}
