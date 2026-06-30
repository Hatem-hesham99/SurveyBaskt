namespace SurveyBaskt.Entities
{
    public sealed class Quesion :AuditableEntity
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;


        public ICollection<Answer> Answers { get; set; } = [];

        public int PollId { get; set; }
        public Poll Poll { get; set; } = default!;
    }
}
