namespace SurveyBaskt.Entities
{
    public class Answer : AuditableEntity
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;



        public int QuesionId { get; set; }
        public Quesion Quesion { get; set; } = default!;
    }
}
