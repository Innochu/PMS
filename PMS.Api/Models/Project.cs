using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PMS.Api.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public int PortfolioId { get; set; }
        [JsonIgnore]
        public Portfolio? Portfolio { get; set; }
    }
}
