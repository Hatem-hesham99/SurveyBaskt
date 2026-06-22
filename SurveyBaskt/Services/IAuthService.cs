using SurveyBaskt.Contracts.Responses;

namespace SurveyBaskt.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
    }
}
