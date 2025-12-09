namespace PMS.Api.Models
{
    public class WorkflowStep
    {
        public Guid Id { get; set; }
        public Guid WorkflowInstanceId { get; set; }
        public WorkflowInstance WorkflowInstance { get; set; } = null!;

        public string StepName { get; set; } = string.Empty; // "Secretariat Review", "DE Signature"
        public string? AssignedRole { get; set; }
        public string? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }

        public string Status { get; set; } = "Pending";
        public int SlaHours { get; set; } = 72;
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? Comments { get; set; }
    }
}
