using Mapster;
using SurveyBaskt.Errors;

namespace SurveyBaskt.Services
{
    public class VoteService(ApplicatonDbContext _dbContext) : IVoteService
    {
        
        public async Task<Result> AddAsync(int pollId, string userId, VoteRequest Request, CancellationToken cancellationToken = default)
        {
            var hasvoted = await  _dbContext.Votes.AnyAsync(v => v.PollId == pollId && v.UserId == userId, cancellationToken);

            if (hasvoted) 
                return Result.Failure(VoteError.UserHasAlreadyAnswered);

            var pollExists = await _dbContext.Polls.AnyAsync(p => p.Id == pollId && p.Ispublished && p.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) && p.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);

            if (!pollExists)
                return Result.Failure(PollError.PollNotFound);
            var availableQuestions = await  _dbContext.Quesions.Where( p => p.PollId == pollId && p.IsActive).Select(q => q.Id).ToListAsync(cancellationToken); 


            if(!Request.VoteAnswers.Select(a=>a.QuesionId).SequenceEqual(availableQuestions))
                return Result.Failure(VoteError.InvalidQuesions);

            var vote = new Vote
            {
                PollId = pollId,
                UserId = userId,
                //VoteAnswers = Request.VoteAnswers.Select(a => new VoteAnswer
                //{
                //    QuestionId = a.QuesionId,
                //    AnswerId = a.AnswerId
                //}).ToList()
                VoteAnswers = Request.VoteAnswers.Adapt<IEnumerable<VoteAnswer>>().ToList()
            };


            await _dbContext.AddAsync(vote,cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
