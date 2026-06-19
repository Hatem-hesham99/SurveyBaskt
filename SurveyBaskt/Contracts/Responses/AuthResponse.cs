namespace SurveyBaskt.Contracts.Responses
{
    public record AuthResponse
    (
        string Id,
        string Token,
        string? Email,
        string FirstName,
        string LastName,
        int ExpiresAt
    );
}
