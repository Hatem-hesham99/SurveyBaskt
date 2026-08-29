using System.Collections.ObjectModel;

namespace SurveyBaskt.Entities
{
    public sealed class Vote : AuditableEntity
    {
        public int Id { get; set; }

        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;


        public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; }
        public int PollId { get; set; }
        public Poll Poll { get; set; } = default!;
    }
}
