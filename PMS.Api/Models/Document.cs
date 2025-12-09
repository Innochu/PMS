using System.Text.Json;

namespace PMS.Api.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty; // Azure Blob or local
        public string DocumentType { get; set; } = string.Empty; // PIN, OAP, BfD, etc.
        public int Version { get; set; } = 1;
        public JsonDocument Metadata { get; set; } = JsonDocument.Parse("{}");
        public bool IsLatest { get; set; } = true;

        public string UploadedById { get; set; } = string.Empty;
        public  User UploadedBy { get; set; } = null!;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public string? DigitalSignatureStatus { get; set; }
    }
}
