using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class ProjectPinValueDriver
    {
        public Guid PinId { get; set; }
        public PinForm Pin { get; set; } = null!;

        [MaxLength(10)]
        public string ValueDriverCode { get; set; } = null!; // e.g., "2.3"

        [MaxLength(100)]
        public string? ValueDriverName { get; set; }
    }
}
