namespace SurveyBaskt.Contracts.Responses
{
    public record PollResponse(
     int Id,
     string Name,
     string Summary,
     bool Ispublished,
     //string? CreatedById,
     //DateTime CreatedAt,
     //string? UpdatedById,
     //DateTime? UpdatedAt,
     DateOnly StartsAt,
     DateOnly EndsAt
     );
    
}
