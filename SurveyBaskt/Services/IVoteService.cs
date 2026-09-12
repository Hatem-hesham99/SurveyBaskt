namespace SurveyBaskt.Services
{
    public interface IVoteService
    {
        Task<Result> AddAsync(int pollId, string userId, VoteRequest Request, CancellationToken cancellationToken=default);
    }
}
