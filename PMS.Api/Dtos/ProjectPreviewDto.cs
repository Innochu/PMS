namespace PMS.Api.Dtos
{
    public class ProjectPreviewDto
    {
        public Guid ProjectId { get; set; }
        public string ProjectNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string PortfolioName { get; set; } = null!;
        public string CurrentPhase { get; set; } = null!;
        public string OverallHealth { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        // PIN Form Status
        public string PinStatus { get; set; } = "Not Started";
        public DateTime? SubmittedAt { get; set; }
        public string? Dg1Decision { get; set; }
        public string? Dg1DecisionBy { get; set; }
        public DateTime? Dg1DecisionAt { get; set; }
        public string? Dg1Comments { get; set; }

        // Form Fields
        public int? ProposalNumber { get; set; }
        public string? Location { get; set; }
        public int? UnitNumber { get; set; }
        public string? ExecutiveSummary { get; set; }
        public string? ProjectObjective { get; set; }
        public string? Remark { get; set; }
        public string? DescriptionOfRAM { get; set; }
        public string? DescriptionOfCO2 { get; set; }
        public HeadOfSafetyDto? HeadOfSafety { get; set; }
    }

    public class HeadOfSafetyDto
    {
        public string? Name { get; set; }
        public string? Identification { get; set; }
    }
}
