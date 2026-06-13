namespace SurveyBaskt.Contracts.Responses
{
    public record PollResponse(
     int Id,
     string Name,
     string Summary,
     bool Ispublished,
     DateOnly StartsAt,
     DateOnly EndsAt
     );
    
}
