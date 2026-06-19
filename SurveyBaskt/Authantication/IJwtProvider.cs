namespace SurveyBaskt.Authantication
{
    public interface IJwtProvider
    {
        (string Token, int expiresIn) GenerateToken(ApplicationUser user);
    }
}
