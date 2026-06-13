using SurveyBaskt.Contracts.Responses;
using System.ComponentModel.DataAnnotations;

namespace SurveyBaskt.Entities
{
    public sealed class Poll
    {
        
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;

        public bool Ispublished { get; set; }
        public DateOnly StartsAt { get; set; }
        public DateOnly EndsAt { get; set; }

    }
}
