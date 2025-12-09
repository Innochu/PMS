namespace PMS.Api.Models
{
    public class WorkflowInstance
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string WorkflowType { get; set; } = string.Empty; // "PIN Approval", "OAP Review"
        public string Status { get; set; } = "Active";
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public ICollection<WorkflowStep> Steps { get; set; } = new List<WorkflowStep>();
    }
}
