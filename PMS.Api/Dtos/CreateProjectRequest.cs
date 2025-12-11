using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Dtos
{
    public class CreateProjectRequest
    {
        [Required] public string Name { get; set; } = null!;
        [Required] public Guid PortfolioId { get; set; }
        [Required] public string CreatedById { get; set; }

        public string ProposalNumber { get; set; }
        public string DefaultNumber { get; set; }
        public string? Location { get; set; }
        public int UnitNumber { get; set; }
        public string? ExecutiveSummary { get; set; }
        public string? ProjectObjective { get; set; } // Fixed: was OjectObjective
        public string? Remark { get; set; }
        public string? DescriptionOfRAM { get; set; }
        public string? HeadOfSafetyName { get; set; }
        public string? HeadOfSafetyIdentification { get; set; }
        public string? DescriptionOfCO2 { get; set; }
    }
}
