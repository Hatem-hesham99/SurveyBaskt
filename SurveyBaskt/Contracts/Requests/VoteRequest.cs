namespace SurveyBaskt.Contracts.Requests
{
    public record VoteRequest(
       
       IEnumerable<VoteAnswerRequest> VoteAnswers
       
    );


}
