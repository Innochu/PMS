using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class ProjectPinAttachment
    {
        [Key]
        public long Id { get; set; }

        public Guid PinId { get; set; }
        public PinForm Pin { get; set; } = null!;

        [MaxLength(200)]
        public string DeliverableName { get; set; } = null!;

        public bool IsAttached { get; set; }
        public string? FilePath { get; set; }           // Recommended: store in cloud/S3
        public string? Remark { get; set; }
        public bool IsBrownfieldHealthCheck { get; set; }
    }
}
