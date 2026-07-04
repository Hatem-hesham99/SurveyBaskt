namespace SurveyBaskt.Contracts.Requests
{
    public record QuesionRequest(
        string Content,
        List<string> Answers
        );
   
}
