using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Dtos
{
    public class CreateProjectRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int PortfolioId { get; set; }
    }
}
