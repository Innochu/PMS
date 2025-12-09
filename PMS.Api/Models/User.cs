using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string role { get; set; } = null!;
        public string Department { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string Status { get; set; }
        public string? Email { get; set; }
    }
}
