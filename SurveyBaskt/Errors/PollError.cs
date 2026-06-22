namespace SurveyBaskt.Errors
{
    public class PollError
    {
        public readonly static Error PollNotFound = new("Poll Not Found", "The requested poll does not exist.");

        public readonly static Error pollDuplicateTitle = new("Poll Duplicate Title", "The poll title already exists. Please choose a different title.");
    }
}
