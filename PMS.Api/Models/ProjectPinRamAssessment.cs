using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class ProjectPinRamAssessment
    {
        [Key]
        public long Id { get; set; }

        public Guid PinId { get; set; }
        public PinForm Pin { get; set; } = null!;

        public string? ScenarioDescription { get; set; }

        // Severity 0–5
        public byte? SeverityPeople { get; set; }
        public byte? SeverityAsset { get; set; }
        public byte? SeverityEnvironment { get; set; }
        public byte? SeverityReputation { get; set; }
        public byte? SeverityCommunity { get; set; }
        public byte? SeveritySecurity { get; set; }

        // Likelihood (1-5 or A-E)
        public string? LikelihoodPeople { get; set; }
        public string? LikelihoodAsset { get; set; }
        public string? LikelihoodEnvironment { get; set; }
        public string? LikelihoodReputation { get; set; }
        public string? LikelihoodCommunity { get; set; }
        public string? LikelihoodSecurity { get; set; }

        public string? OverallRiskScore { get; set; } // Negligible, Low, Medium, High

        // Head of Safety Sign-off
        public string? HeadSafetyName { get; set; }
        public string? HeadSafetyRef { get; set; }
        public byte[]? HeadSafetySignature { get; set; } // or string path to file
        public DateOnly? HeadSafetyDate { get; set; }
    }
}
