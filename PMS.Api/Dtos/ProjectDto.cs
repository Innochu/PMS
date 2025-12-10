namespace PMS.Api.Dtos
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid PortfolioId { get; set; }
        public string ProjectNumber { get; set; }
    }
}
