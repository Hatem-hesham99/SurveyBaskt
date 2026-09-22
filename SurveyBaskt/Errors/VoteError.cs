namespace SurveyBaskt.Errors
{
    public class VoteError
    {
        public static readonly Error UserHasAlreadyAnswered = new Error("Vote.UserHasAlreadyAnswered", "User has already answered this poll.");
        public static readonly Error InvalidQuesions = new Error("Vote.InValidQuesions", "The provided questions are not valid for this poll.");
    }
}
