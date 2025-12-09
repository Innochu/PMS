using System.Text.Json;

namespace PMS.Api.Models
{
    // 5. PcapForm.cs  (Project Controls Assurance Plan – one per project, versioned)
    public class PcapForm
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int Version { get; set; } = 1;
        public string Phase { get; set; } = "Assess";

        // Full PCAP form content (all sections, tables, etc.)
        public JsonDocument FormData { get; set; } = JsonDocument.Parse("{}");

        // Important copied data from Global DAM (stored for auditability)
        public JsonDocument GlobalDamControls { get; set; } = JsonDocument.Parse("[]"); // array of controls
        public string? SelectedAssuranceProducts { get; set; }   // comma-separated or JSON array
        public string? ValueImprovingPractices { get; set; }

        public string Status { get; set; } = "Draft";
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedById { get; set; }
        public  User? ApprovedBy { get; set; }

        public string? DigitalSignatureStatus { get; set; }
        public DateTime? SignedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = null!;
    }
}
