using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class ProjectPinInterfaceSignOff
    {
        [Key]
        public Guid PinId { get; set; }

        public PinForm Pin { get; set; } = null!;

        public string? TfiName { get; set; }
        public string? TfiRef { get; set; }
        public byte[]? TfiSignature { get; set; }
        public DateOnly? TfiDate { get; set; }
    }
}
