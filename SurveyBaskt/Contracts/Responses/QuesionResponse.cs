namespace SurveyBaskt.Contracts.Responses
{
    public record QuesionResponse(
          int Id,
        string Content,
        IEnumerable<AnswerResponse> Answers
        );
    
}
