using PMS.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PMS.Api.Models
{
    // 1. Project.cs - Core SSOT Entity
    public class Project
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
      //  public string Name { get; set; } = null!;

        public Guid PortfolioId { get; set; }
        [JsonIgnore]
        public Portfolio? Portfolio { get; set; }
        public string ProjectNumber { get; set; } = string.Empty; // e.g., 25MOCPDR001
       // public string? TempProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ProjectType { get; set; } // Roadmap 1/2/3
        public string? LocationArea { get; set; }
        public decimal? ApprovedCostUsd { get; set; }
        [MaxLength(100)]
        public string? ProjectLocationArea { get; set; }

        [MaxLength(50)]
        public string? TrainUnitNo { get; set; }
        public string? ComplexityLevel { get; set; }
        public string CurrentPhase { get; set; } = "Identify"; // Enum better in prod
        public string? OverallHealth { get; set; } // Green/Amber/Red
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateOnly? StartDate { get; set; }
        public DateOnly? CompletionDate { get; set; }
        public string CreatedById { get; set; } = string.Empty;
        public  User CreatedBy { get; set; } = null!;
        public ProjectRoadmap Roadmap { get; set; }
        // Integrations
        public string? SapWbsCode { get; set; }
        public string? SapSyncStatus { get; set; }
        public DateTime? NextDgDate { get; set; }

        // Navigation
        public PinForm? PinForm { get; set; }
        public ICollection<OapForm> OapForms { get; set; } = new List<OapForm>();
        public ICollection<PcapForm> PcapForms { get; set; } = new List<PcapForm>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
        public ICollection<ActionItem> ActionItems { get; set; } = new List<ActionItem>();
        public ICollection<DrbDecision> DrbDecisions { get; set; } = new List<DrbDecision>();
        public ICollection<WorkflowInstance> Workflows { get; set; } = new List<WorkflowInstance>();
    }
}