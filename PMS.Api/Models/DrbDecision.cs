namespace PMS.Api.Models
{
    public class DrbDecision
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string Gate { get; set; } = string.Empty; // DG1, DG2, etc.
        public string Decision { get; set; } = string.Empty;
        public string DecidedById { get; set; } = string.Empty;
        public User DecidedBy { get; set; } = null!;
        public DateTime DecidedAt { get; set; }
        public string? Comments { get; set; }
        public string? EvidencePackageUrl { get; set; }
    }
}
