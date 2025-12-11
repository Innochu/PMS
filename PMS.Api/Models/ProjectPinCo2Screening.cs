using System.ComponentModel.DataAnnotations;

namespace PMS.Api.Models
{
    public class ProjectPinCo2Screening
    {
        [Key]
        public long Id { get; set; }

        public Guid PinId { get; set; }
        public PinForm Pin { get; set; } = null!;

        public string? ScenarioDescription { get; set; }
        public decimal? ExpectedCo2ChangeTonnePerYear { get; set; } // +ve = increase, -ve = reduction

        public string? PsuName { get; set; }
        public byte[]? PsuSignature { get; set; }
        public DateOnly? PsuDate { get; set; }

        public decimal? ExpectedCostUsdPerYear { get; set; }
        public string? CteName { get; set; }
        public byte[]? CteSignature { get; set; }
        public DateOnly? CteDate { get; set; }

        public decimal? Co2AbatementCostUsdPerTonne { get; set; }
    }
}
