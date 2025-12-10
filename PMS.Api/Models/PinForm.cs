using System.Text.Json;

namespace PMS.Api.Models
{
    public class PinForm
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public JsonDocument Section1Data { get; set; } = JsonDocument.Parse("{}");
        public JsonDocument Section2Data { get; set; } = JsonDocument.Parse("{}"); // Cost, Schedule, Risk
        public JsonDocument SpecialistInputs { get; set; } = JsonDocument.Parse("{}");

        public string Status { get; set; } = "Draft"; // Draft, Submitted, Under Review, DG1 Scheduled, Approved...
        public DateTime? SubmittedAt { get; set; }

        // DG1 Decision
        public string? Dg1Decision { get; set; } // Approve, Conditional, Reject, Represent
        public string? Dg1DecisionById { get; set; }
        public  User? Dg1DecisionBy { get; set; }
        public DateTime? Dg1DecisionAt { get; set; }
        public string? Dg1Comments { get; set; }
    }
}
