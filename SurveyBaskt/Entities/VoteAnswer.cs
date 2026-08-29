namespace SurveyBaskt.Entities
{
    public sealed class VoteAnswer : AuditableEntity
    {
        public int Id { get; set; }
        public int VoteId { get; set; }
        public Vote Vote { get; set; } = default!;
        public int QuestionId { get; set; }
        public Quesion Question { get; set; } = default!;
        public int AnswerId { get; set; }
        public Answer Answer { get; set; } = default!;
    }
}
