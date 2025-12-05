namespace PMS.Api.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PortfolioId { get; set; }
    }
}
