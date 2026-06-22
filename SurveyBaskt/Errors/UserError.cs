namespace SurveyBaskt.Errors
{
    public class UserError
    {
        public readonly static Error InvalidCredentials = new("Invalid Credentials", "Invalid Email / Password");
    }
}
