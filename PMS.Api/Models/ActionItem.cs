namespace PMS.Api.Models
{
    public class ActionItem
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? AssignedToId { get; set; }
        public  User? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = "Open";
        public string SourceType { get; set; } = string.Empty; // Workflow, Workshop, etc.
        public Guid? SourceId { get; set; }
        public bool Escalated { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
