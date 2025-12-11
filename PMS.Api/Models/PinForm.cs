using System.Text.Json;

namespace PMS.Api.Models
{
    public class PinForm
    {
        public Guid Id { get; set; }

        // SECTION 1.1 - General Detail
        public string? ProposalNo { get; set; }                    // PPDYYXXX
        public string? MtoNo { get; set; }
        public DateOnly? DateRegistered { get; set; }
        public string? ProposedProjectNo { get; set; }     // e.g., "25/MOC/PDR/001P"
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public string? ExecutiveSummary { get; set; }
        public string? ProjectObjective { get; set; }
        public string? ShutdownRemarks { get; set; }
        public string? InterfaceDetails { get; set; }
        public string? ProblemStatements { get; set; }
        public string? BusinessImpactAnalysis { get; set; }
        public string? FinancialBenefitsAnalysis { get; set; }
        public string? OpportunityCostNonImplementation { get; set; }
        public string? ValueAtRisk { get; set; }
        public string? CurrentMitigations { get; set; }
        public string? FactsAndAssumptions { get; set; }
        //public JsonDocument Section1Data { get; set; } = JsonDocument.Parse("{}");
        //public JsonDocument Section2Data { get; set; } = JsonDocument.Parse("{}"); // Cost, Schedule, Risk
        //public JsonDocument SpecialistInputs { get; set; } = JsonDocument.Parse("{}");


        // Value Drivers, RAM, CO2, MTO, Stakeholders, Attachments stay here

        public List<ProjectPinValueDriver> ValueDrivers { get; set; } = new();
        public ProjectPinRamAssessment? RamAssessment { get; set; }
        public ProjectPinCo2Screening? Co2Screening { get; set; }
        public ProjectPinMtoScore? MtoScore { get; set; }
        public ProjectPinInterfaceSignOff? InterfaceSignOff { get; set; }


        public string Status { get; set; } = "Draft"; // Draft, Submitted, Under Review, DG1 Scheduled, Approved...
        public DateTime? SubmittedAt { get; set; }

        // DG1 Decision
        public string? Dg1Decision { get; set; } // Approve, Conditional, Reject, Represent
        public string? Dg1DecisionById { get; set; }
        public  User? Dg1DecisionBy { get; set; }
        public DateTime? Dg1DecisionAt { get; set; }
        public string? Dg1Comments { get; set; }


        public ICollection<ProjectPinStakeholder> Stakeholders { get; set; }
        public ICollection<ProjectPinAttachment> Attachments { get; set; }
        public bool IsBreakIn { get; set; }
        public string? DrbMeetingRef { get; set; }
        public DateOnly? DrbApprovalDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }

    }
}

