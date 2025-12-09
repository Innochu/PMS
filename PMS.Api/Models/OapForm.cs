using System.Text.Json;

namespace PMS.Api.Models
{
    public class OapForm
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int Version { get; set; } = 1;                    // 1 = Identify/Assess, 2 = Select, 3 = Define, etc.
        public string Phase { get; set; } = "Assess";            // for readability

        // All form data is stored flexibly in JSONB (PostgreSQL) or NVARCHAR(MAX) JSON (SQL Server)
        // This gives us 100% prototype speed while still being future-proof
        public JsonDocument FormData { get; set; } = JsonDocument.Parse("{}");

        // Key fields we will query/report on frequently (denormalised for performance)
        public string? Roadmap { get; set; }                     // Roadmap 1 / 2 / 3
        public string? AssuranceLevel { get; set; }              // Self / Focused / Premium
        public string? OpportunityCharacteristics { get; set; }
        public DateTime? FirstContactMeetingDate { get; set; }

        public string Status { get; set; } = "Draft";            // Draft → Submitted → In Review → Approved → Superseded
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedById { get; set; }
        public User? ApprovedBy { get; set; }

        // Digital signature tracking
        public string? DigitalSignatureStatus { get; set; }      // Pending / Completed / Rejected
        public DateTime? SignedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = null!;
    }
}
