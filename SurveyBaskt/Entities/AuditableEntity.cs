namespace SurveyBaskt.Entities
{
    public class AuditableEntity
    {
        public string CreatedById { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 

        public string? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ApplicationUser CreatedBy { get; set; } = default!;

        public ApplicationUser? UpdatedBy { get; set; } = null!;
    }
}
