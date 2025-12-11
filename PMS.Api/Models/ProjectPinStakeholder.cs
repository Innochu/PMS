using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class ProjectPinStakeholder
    {
        [Key]
        public long Id { get; set; }

        public Guid PinId { get; set; }
        public PinForm Pin { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Role { get; set; } = null!; // Originator, Asset Holder, BOM, etc.

        public string? Name { get; set; }
        public string? RefInd { get; set; }
        public byte[]? Signature { get; set; }
        public DateOnly? SignedDate { get; set; }
    }
}
