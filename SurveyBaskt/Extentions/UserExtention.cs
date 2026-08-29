using System.Security.Claims;

namespace SurveyBaskt.Extentions
{
    public static class UserExtention
    {
        public static string? GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
