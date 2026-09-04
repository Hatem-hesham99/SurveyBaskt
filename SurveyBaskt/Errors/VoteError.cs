namespace SurveyBaskt.Errors
{
    public class VoteError
    {
        public static readonly Error UserHasAlreadyAnswered = new Error("Vote.UserHasAlreadyAnswered", "User has already answered this poll.");
    }
}
