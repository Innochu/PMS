using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class Portfolio
    {
        public int Id { get; set; }

        [Required]
        public string PortfolioNumber { get; set; } = null!;

        public string? Department { get; set; }

        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
