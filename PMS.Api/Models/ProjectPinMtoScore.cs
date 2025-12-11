using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class ProjectPinMtoScore
    {
        [Key]
        public long Id { get; set; }

        public Guid PinId { get; set; }
        public PinForm Pin { get; set; } = null!;

        public string? MtoScore { get; set; }
        public string? AssessorName { get; set; }
        public string? AssessorRef { get; set; }
        public byte[]? AssessorSignature { get; set; }
        public DateOnly? AssessmentDate { get; set; }
    }
}
