using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class User
    { 
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string role { get; set; } = null!;
        public string Department { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

        public string? Email { get; set; }
    }
}
