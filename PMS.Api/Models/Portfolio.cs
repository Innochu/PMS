using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class Portfolio
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string PortfolioName { get; set; } = null!;

        public string? Department { get; set; }

        public ICollection<Project> Projects { get; set; } = new List<Project>();
       
    }
}
