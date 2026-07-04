namespace SurveyBaskt.Errors
{
    public class QuesionError
    {
        public static readonly Error QuesionNotFound = new("Quesion Not Found", "The requested quesion does not exist.");
        public static readonly Error DubliateContent = new("Quesion Duplicate Content", "The quesion content already exists. Please choose a different content.");
    }
}
