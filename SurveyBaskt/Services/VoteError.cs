namespace SurveyBaskt.Services
{
    public class VoteError
    {
        public static readonly Error UserHasAlreadyAnswered = new Error("Vote.UserHasAlreadyAnswered", "User has already answered this poll.");
    }
}
